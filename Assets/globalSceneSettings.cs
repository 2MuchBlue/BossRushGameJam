using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalSceneSettings : MonoBehaviour
{
    MasterSettings masterSettings;
    public float bpm = 60;

    public AudioSource[] PlayOnStart;

    void Awake(){
        Debug.Log("Starting Scene Functions Running");
        GameObject mSetsObj = GameObject.FindGameObjectWithTag("MasterSettings");
        if(mSetsObj != null){ 
            Debug.Log("Got past finding the object");
            masterSettings = mSetsObj.GetComponent<MasterSettings>();
            if(masterSettings != null){ 
                Debug.Log("got passed finding component");
                startAudios2vol(masterSettings.musicVol * 0.01f);
            }else{
                startAudios2vol(1);
            }
            
        }else{
            startAudios2vol(1);
        }
        
    }

    void startAudios2vol(float vol){
        for(int i = 0; i < PlayOnStart.Length; i++){
            Debug.Log("working on source " + i + " of " + PlayOnStart.Length);
            PlayOnStart[i].volume = vol;
            PlayOnStart[i].Play();
        }
    }
}
