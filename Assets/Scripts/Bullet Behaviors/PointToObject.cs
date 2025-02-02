using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToObject : MonoBehaviour
{
    [SerializeField]
    GameObject pointTo;

    [SerializeField] string lookForTag = ""; // if not empty, will set <pointTo> to any found GameObject 

    void Start(){
        if(lookForTag != ""){
            pointTo = GameObject.FindGameObjectWithTag(lookForTag);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles = new Vector3 (0, 0, vecToAngle(transform.position - pointTo.transform.position));
    }

    float vecToAngle(Vector3 vec){
        vec = Vector3.Normalize(vec);
        float offset = 0;
        if(vec.x < 0){
            offset = 180;
        }
        return Mathf.Atan(vec.y/vec.x) * Mathf.Rad2Deg + offset + 180;
    }
}
