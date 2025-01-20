using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCircle_BulletBehavior : MonoBehaviour
{

    [SerializeField]
    bool moving = false;
    public float speed;
    float momentum = -5;

    public float lifeTime = 5;

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(moving){
            momentum += speed * Time.deltaTime;
            transform.position += transform.right * momentum * Time.deltaTime;
        }
        if(Vector3.Distance(transform.position, Vector3.zero) > 45){
            Destroy(this.gameObject);
        }
    }

    public void Go(){
        moving = true;
        Invoke("killBullet", lifeTime);
    }

    void killBullet(){
        Destroy(this.gameObject);
    }
}
