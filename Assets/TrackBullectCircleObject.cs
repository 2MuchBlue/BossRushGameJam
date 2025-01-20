using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackBullectCircleObject : MonoBehaviour
{
    public GameObject circleObjectToTrack;
    public string tagOfSourceObject; // the heart that this is supposte to originate from;
    GameObject SourceObject;
    GlideToPosition glide;
    // Start is called before the first frame update
    void Start()
    {
        SourceObject = GameObject.FindGameObjectWithTag(tagOfSourceObject);
        if(SourceObject != null){
            transform.position = SourceObject.transform.position;
        }else{
            transform.position = new Vector3(0, 0, 15);
        }
        glide = GetComponent<GlideToPosition>();
    }

    // Update is called once per frame
    void Update()
    {
        glide.setTargetPos(circleObjectToTrack.transform.position);
    }
}
