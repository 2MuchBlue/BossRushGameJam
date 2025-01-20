using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void quitGame(){
        if(Application.isEditor){
            Debug.Log("Can't quit, in editor");
            
        }else{
            Application.Quit();
        }
    }
}
