using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterSettings : MonoBehaviour
{
    void Awake(){
        DontDestroyOnLoad(this.gameObject);
    }

    public float musicVol = 100;
    
}
