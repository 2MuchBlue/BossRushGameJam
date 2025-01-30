using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TinyJson;
using Unity.Mathematics;
using UnityEngine.Events;


public class ReadSongJson : MonoBehaviour
{
    public TextAsset apple;
    byte[] appleBytes;
    object appleJson;

    [SerializeField]
    float bpm = -1;
    [SerializeField]
    float ticksPerBeat = -1;

    [SerializeField, Range(0, 24)]
    int songLength = 0; // pattern count in total
    
    [SerializeField, Range(0, 8)]
    int channel2compile = 0;

    [SerializeField]
    channel songObject;

    [Space, Header("Attacks (triggered by volume)")]
    public jsonVolumeAttack volAttack;

    [Space, SerializeField]
    bool recompJson = false;
    
    // Start is called before the first frame update
    void Start()
    {
        appleBytes = apple.bytes;
        appleJson = apple.text.FromJson<object>();

        /*
        //Debug.Log((List<object>)searchJsonObject(appleJson, new object[]{ "channels", 1 }));
        searchJsonObject(appleJson, new object[]{ "channels", 1, "patterns", 0, "notes", 0, "points", 1, "tick"  });
        */

        compileMusicJson();
    }

    note searchForNoteAtTime(float time, int pattern){
        float ticksTime = secs2ticks(time) % 32;
        for(int i = 0; i < songObject.patterns[pattern].notes.Length; i++ ){
            note item = songObject.patterns[pattern].notes[i];
            //Debug.Log(i + ", " + item.startTick + ", " + item.endTick + ", now: " + ticksTime + ", " + (ticksTime <= item.endTick && ticksTime >= item.startTick));
            if(shouldTriggerinMyDuration(item, time)){
                //Debug.Log("should trigger");
                return songObject.patterns[pattern].notes[i];
            }
            ////Debug.Log("is here but not right now.");
        }

        return null;
    }

    void compileMusicJson(){

        bpm = (int)searchJsonObject(appleJson, new object[]{ "beatsPerMinute" });
        ticksPerBeat = (int)searchJsonObject(appleJson, new object[]{ "ticksPerBeat" });

        List<channel> channels = new List<channel>();
        //for(int i = melodyLayers; i < melodyLayers + drumLayers - 1; i++){
            List<pattern> patterns = new List<pattern>();
            for(int iPattern = 0; iPattern < songLength; iPattern++ ){
                patterns.Add(tryGetPatternInJsonObject(appleJson, channel2compile, iPattern));
            }
            songObject = new channel(patterns.ToArray());
            
            int iSeq = 0;
            object seqItem = null;
            List<int> seqList = new List<int>();
            do {
                seqItem = searchJsonObject(appleJson, new object[]{ "channels", channel2compile, "sequence", iSeq});
                if(seqItem == null){ seqItem = -1; }else{
                    seqList.Add((int)seqItem);
                }
                iSeq++;
            }
            while((int)seqItem >= 0 || iSeq < 32 );
            songObject.sequence = seqList.ToArray();
        //}
    }

    class ByteFuncClass
    {
        // | 128 | 64 | 32 | 16 | 8 | 4 | 2 | 1 |
        public int Bit1 = 1;   // 0000 0001
        public int Bit2 = 2;   // 0000 0010
        public int Bit3 = 4;   // 0000 0100
        public int Bit4 = 8;   // 0000 1000
        public int Bit5 = 16;  // 0001 0000
        public int Bit6 = 32;  // 0010 0000
        public int Bit7 = 64;  // 0100 0000
        public int Bit8 = 128; // 1000 0000
        public int FirstNibble = 240; // 1111 0000
        public int LastNibble = 31; // 0000 1111
        public void getFirstBit(byte searchByte){

        }
    }

    [Serializable]
    public class jsonVolumeAttack{
        [Range(0, 100)]
        public int volume = 100;
        public UnityEvent onTrigger;
    }

    [Serializable]
    class note {
        public int startTick;
        public int endTick;
        public int volume = 100;
        public int duration;
        public note(int startTick_, int endTick_ ){
            startTick = startTick_;
            endTick = endTick_;
            duration = endTick_ - startTick_;
        }

        public bool calledRecently = false;
    }

    bool shouldTriggerinMyDuration(note n0te, float time){
        float ticksTime = secs2ticks(time) % 32;
        if( ticksTime <= n0te.endTick && ticksTime >= n0te.startTick){
            if(!n0te.calledRecently){
                //Debug.Log("this should be triggered only once");
                n0te.calledRecently = true;
                return true;
            }else{
                n0te.calledRecently = true;
                return false;
            }
        }else{
            n0te.calledRecently = false;
            return false;
        }
    }

    [Serializable]
    class pattern {
        public note[] notes;

        public pattern(note[] _notes){
            notes = _notes;
        }
    }

    [Serializable]
    class channel{
        public pattern[] patterns;
        public int[] sequence; // list of ints that represent the patterns; subtract one to get the list 
        public string type;
        public channel(pattern[] _patterns){
            patterns = _patterns;
        }
    }

    class MidiDecoding{
        void findChunk(byte[] bytes){
            
            for(int i = 0; i < bytes.Length; i++){
                byte item = bytes[i];
                if(item + Convert.ToByte(77) == 0 ){
                    
                }
            }
        }
    }

    ByteFuncClass byteFuncs = new ByteFuncClass();

    float secs2ticks(float time){
        return ticksPerBeat / (60 / bpm) * time;
    }

    // Update is called once per frame
    void Update()
    {

        ////Debug.Log(secs2ticks(Time.timeSinceLevelLoad));

        if(recompJson){
            recompJson = false;
            compileMusicJson();
        }

        note checkNote = null;
        try
        {
            float patternTime = (secs2ticks(Time.timeSinceLevelLoad) * 0.03125f) % (songObject.sequence.Length - 1);
            int seqIndex = (int)Mathf.Floor(patternTime); // index for the sequence list
            int seqItem = songObject.sequence[ seqIndex ] - 1; // result of sequence grab; one is subtracted because boopbox starts pattern indexes at 1, but the first item is at 0;
                //Debug.Log("channel: " + channel2compile + " || sequence index: " + seqIndex + " || result: " + seqItem);
            if(seqItem < 0) { return; } // if it is less then 0 (-1) then just skip this and leave
                //Debug.Log("checking for notes, tickTime: " + patternTime );
            checkNote = searchForNoteAtTime(Time.timeSinceLevelLoad, seqItem );
            if(checkNote != null){
                volAttack.onTrigger.Invoke();
            }else{
                //Debug.Log("note was not found (tryed, failed)");
            }
        }
        catch (System.Exception)
        {
            //Debug.LogWarning("nothing was found");
            checkNote = null;
        }

        /*
        var test = apple.text.FromJson<object>();
        // ( (* cast * (rest of the json) ) )
        var number = (List<object>)((Dictionary<string, object>)((List<object>)((Dictionary<string, object>)test)["channels"])[1])["patterns"];
        var num2 = ((Dictionary<string, object>)((List<object>)number)[0])["notes"];


//        var output = 
//        getKeyFromJsonObject(
//            getIndexFromJsonArray(
//                getKeyFromJsonObject(
//                    getIndexFromJsonArray(
//                        getKeyFromJsonObject(
//                            getIndexFromJsonArray(
//                                getKeyFromJsonObject(
//                                    getIndexFromJsonArray(
//                                        getKeyFromJsonObject(
//                                            test, "channels"
//                                        ), 1 /* drum index */
//                                    ), "patterns"
//                                ), 0 /* pattern index */
//                            ), "notes"
//                        ), 0
//                    ), "points"
//                ), 0 /* note index */
//            ), "tick"
//        );

/*
        var source = "{\"apple\" : \"seven\"}".FromJson<object>();
        var appleTag = ((Dictionary<string, List<int>>)source)["apple"];
        var output = appleTag;
*/

        ////Debug.Log(searchJsonObject(test, new object[]{"channels", 1, "patterns", 0, "notes", 0, "points", 1, "tick" }));
        ////Debug.Log(Convert.ToString(appleBytes[0] & byteFuncs.FirstNibble, 2));
    }

    object getKeyFromJsonObject( object jsonObj, string key ){
        object localJsonObj = ((Dictionary<string, object>)jsonObj)[key];
        if(localJsonObj != null){
            return localJsonObj;
        }
        return null;
    }

    object getIndexFromJsonArray(object jsonObj, int index){
        object localJsonObj = ((List<object>)jsonObj)[index];
        if(localJsonObj != null){
            return localJsonObj;
        }
        return null; 
    }

    object jsonArrayToList(object jsonObject){
        return (List<object>)jsonObject;
    }

    object searchJsonObject(object jsonObject, object[] keys){
        object localJsonObject = jsonObject;
        for(int i = 0; i < keys.Length; i++ ){
            try
            {
                if(keys[i].GetType() == typeof(int)){
                    localJsonObject = getIndexFromJsonArray(localJsonObject, (int)keys[i]);
                }
                if(keys[i].GetType() == typeof(string)){
                    localJsonObject = getKeyFromJsonObject(localJsonObject, (string)keys[i]);
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        return localJsonObject;
    }

    /*note[] getNotesFromJsonObject(object jsonObject){
        //, 1, "patterns", 0, "notes", 0, "points", 1, "tick" 
        
    }*/

    note tryGetNoteInJsonObject(object jsonObject, int channel, int pattern, int note){
        object noteJson = searchJsonObject(jsonObject, new object[]{ "channels", channel, "patterns", pattern, "notes", note });
        if(noteJson == null){ return null; }
        object notePoints = getKeyFromJsonObject(noteJson, "points");

        //Debug.Log((int)searchJsonObject(notePoints, new object[]{ 1, "tick" }));

        return new note( (int)searchJsonObject(notePoints, new object[]{ 0, "tick" }), (int)searchJsonObject(notePoints, new object[]{ 1, "tick" }) );
    }

    pattern tryGetPatternInJsonObject(object jsonObject, int channel, int pattern){
        note activeNote = null;
        List<note> notes = new List<note>();
        int iChannel = channel;
        int iPattern = pattern;
        int iNote = 0;
        do {
            activeNote = tryGetNoteInJsonObject(jsonObject, iChannel, iPattern, iNote);
            if(activeNote != null){
                notes.Add(activeNote);
            }
            iNote++;
        } 
        while (activeNote != null);
        
        return new pattern(notes.ToArray());
    }


}
