using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatChanger : MonoBehaviour
{
    public int mat2change = 0;
    public Renderer meshRenderer;
    
    public void changeMat2(Material mat){
        Debug.Log("trying to change " + meshRenderer.gameObject.name + "'s material at index " + mat2change + " to " + mat.name);
        Material[] originalMatArray = meshRenderer.materials;
        originalMatArray[mat2change] = mat;
        meshRenderer.materials = originalMatArray;
    }
}
