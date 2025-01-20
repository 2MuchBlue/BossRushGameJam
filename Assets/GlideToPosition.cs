using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideToPosition : MonoBehaviour
{
    public Vector3 targetPos;
    public float speed = 10;
    public void setTargetPos(Vector3 pos){
        targetPos = pos;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += ( targetPos - transform.position ) * speed * Time.deltaTime;
    }
}
