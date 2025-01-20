using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicBulletMovement : MonoBehaviour
{
    public float speed = 1;

    float startLife = 0;
    public float lifetime = 10; 

    // Start is called before the first frame update
    void Start()
    {
        startLife = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        if(startLife + lifetime < Time.realtimeSinceStartup){
            bulletExpire();
        }
    }

    void bulletExpire(){
        Destroy(this.gameObject);
    }
}
