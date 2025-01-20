using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BulletBox : MonoBehaviour
{
    public GameObject bulletWall;
    GameObject[] bullets;
    public float attackTime = 5; // seconds the attack lasts

    public Vector2 halfRect = new Vector2(3, 3);
    //Vector2 boundingBox = new Vector2(5, 5); // half extents of the containing box

    // Start is called before the first frame update
    void Start()
    {
        bullets = new GameObject[4];
        bullets[0] = SpawnWullet(bulletWall, transform.position + (transform.right * halfRect.x), transform.rotation);
        bullets[1] = SpawnWullet(bulletWall, transform.position + (transform.right * -halfRect.x), transform.rotation);
        bullets[2] = SpawnWullet(bulletWall, transform.position + (transform.up * halfRect.y), Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 90)));
        bullets[3] = SpawnWullet(bulletWall, transform.position + (transform.up * -halfRect.y), Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, 90)));
        
        Invoke("endAttack", attackTime);
    }

    // Update is called once per frame
    void Update()
    {
        bullets[0].transform.position = transform.position + (bullets[0].transform.right *  halfRect.x);
        bullets[1].transform.position = transform.position + (bullets[1].transform.right * -halfRect.x);
        bullets[2].transform.position = transform.position + (bullets[2].transform.up *  halfRect.y);
        bullets[3].transform.position = transform.position + (bullets[3].transform.up * -halfRect.y);
    }

    void endAttack(){
        Destroy(this.gameObject);
    }

    GameObject SpawnWullet(GameObject bullPat, Vector3 position, Quaternion rotation){
        GameObject bullet = Instantiate(
            bullPat, 
            position, 
            rotation
        );

        return bullet;
    }
}
