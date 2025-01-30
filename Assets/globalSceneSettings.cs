using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globalSceneSettings : MonoBehaviour
{
    MasterSettings masterSettings;
    public float bpm = 60;

    public AudioSource[] PlayOnStart;

    void Start(){
        masterSettings = GameObject.FindGameObjectWithTag("MasterSettings").GetComponent<MasterSettings>();
        for(int i = 0; i < PlayOnStart.Length; i++){
            PlayOnStart[i].volume = masterSettings.musicVol;
            PlayOnStart[i].Play();
        }
    }
}
