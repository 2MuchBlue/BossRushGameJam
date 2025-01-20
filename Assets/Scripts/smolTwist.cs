using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smolTwist : MonoBehaviour
{
    [Range(-180, 180)]
    public float spinSpeed = 2;
    [Range(-10, 10)]
    public float swayAmount = 10;

    public float offset = 15;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3(0, 0, Mathf.Sin(Time.realtimeSinceStartup * spinSpeed * Mathf.Deg2Rad) * swayAmount + offset);
    }
}
