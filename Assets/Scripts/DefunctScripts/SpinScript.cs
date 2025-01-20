using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SpinScript : MonoBehaviour
{
    public float RotPerMin = 1;
    public float offsetRot = 0;
    public Vector3 rotation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = rotation * ((Time.realtimeSinceStartup / 60 * RotPerMin * 360) + offsetRot);
    }
}
