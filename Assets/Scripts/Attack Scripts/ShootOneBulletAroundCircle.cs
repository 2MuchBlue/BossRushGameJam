using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootOneBulletAroundCircle : MonoBehaviour
{
    
    public float radiusFromCenter = 10;
    public GameObject lazerObject;

    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
        UnityEngine.Random.InitState(this.gameObject.scene.GetHashCode() * (int)Time.timeSinceLevelLoad);
        if (player == null){
            player = GameObject.FindGameObjectsWithTag("Player")[0];
            if(player == null){
                Destroy(this.gameObject);
            }
        }

        ShootAttempt();
    }

    void ShootAttempt(){

        transform.position = transform.right * radiusFromCenter;

        transform.eulerAngles = new Vector3(0, 0, vecToAngle(transform.position - player.transform.position));
    
        Shoot();
        Destroy(this.gameObject);
    }

    void Shoot(){
        Instantiate(lazerObject, transform.position, transform.rotation);
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
