using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraMatAplication : MonoBehaviour
{
    public Material[] CamMats;
    
    private void OnRenderImage(RenderTexture source,RenderTexture dest){
        for(int i = 0; i < CamMats.Length; i++){
            if(CamMats[i] == null){
                Graphics.Blit(source, dest);
                return;
            }
            Graphics.Blit(source, dest, CamMats[i]);
        }
        
    }
}
