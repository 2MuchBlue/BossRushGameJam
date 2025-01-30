using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class masterAudioControl : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField]
    AudioSource[] sources;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        int sample = audioSource.timeSamples;
        for(int i = 0; i < sources.Length; i++ ){
            sources[i].timeSamples = sample;
        }
    }
}
