using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnBeat : MonoBehaviour
{
    public AudioSource audioSource;
    [SerializeField] float bpm = 150;
    [SerializeField] float ticksPerBeat = 4;
    [SerializeField] float tick2modAt = 64;

    [SerializeField] trigger[] triggers;

    [SerializeField] float tickTime = 0;

    [Serializable]
    class trigger {
        public UnityEvent OnMyBeat;
        public float tick2trigger = 0;
        public float durration = 4; // durration of note in ticks
        public float localTick2modAt = -1; // tick to wrap around, if negative uses global one from script head

        public trigger(int tick2trigger_){
            tick2trigger = tick2trigger_;
        }

        bool inDurration(float ticks){
            ticks = ticks % localTick2modAt;
            return ( ticks >= tick2trigger && ticks <= tick2trigger + durration );
        }

        public bool shouldTrigger( float ticks ){
            bool weIn = inDurration(ticks); // if we are in this trigger's duration and time.
            if( weIn ){
                if( ! triggeredReasently ){
                    triggeredReasently = true;
                    return true;
                }else{
                    return false;
                }
            }else{
                triggeredReasently = false;
                return false;
            }
        }

        public bool triggeredReasently = false;
    }

    float secs2ticks(float time){
        return ticksPerBeat / (60 / bpm) * time;
    }

    void Start() {
        for(int i = 0; i < triggers.Length; i++){
            if(triggers[i].localTick2modAt < 0){
                triggers[i].localTick2modAt = tick2modAt;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        float globalTime = (float)audioSource.timeSamples / (float)audioSource.clip.frequency;
        float globalTicks = secs2ticks(globalTime);
        tickTime = globalTicks % tick2modAt;
        for(int i = 0; i < triggers.Length; i++){
            if( triggers[i].shouldTrigger( globalTicks ) ) {
                triggers[i].OnMyBeat.Invoke();
            }
        }
    }

    public void triggerTriggerAtIndex(int index){ // triggers <index> trigger's trigger
        triggers[index].OnMyBeat.Invoke();
    }
}
