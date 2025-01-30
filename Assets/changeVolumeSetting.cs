using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeVolumeSetting : MonoBehaviour
{
    [SerializeField]
    Slider volSlider;
    [SerializeField]
    MasterSettings masterSettings;
    public void setVolume(){
        masterSettings.musicVol = volSlider.value;
    }
}
