using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string sceneToChangeTo;
    
    public void tryChangeingScene(){
        SceneManager.LoadSceneAsync(sceneToChangeTo);
    } 
}
