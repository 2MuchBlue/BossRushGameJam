using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerShooter : MonoBehaviour
{
    public BulletPattern[] lazers;
    GameObject[] lazerObjects;
    // Start is called before the first frame update
    void Start()
    {
        lazerObjects = new GameObject[lazers.Length];
        for(int i = 0; i < lazers.Length; i++){
            lazers[i].bulletPrefab = Instantiate(
                lazers[i].bulletPrefab, 
                transform.position, 
                Quaternion.Euler(
                    transform.eulerAngles + lazers[i].offsetRotation
                ),
                this.transform
            );
            
            if(lazers[i].bulletPrefab.TryGetComponent<BasicBulletMovement>(out BasicBulletMovement bulletMovement)){
                //bulletMovement.canHit = lazers[i].canHitMask;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < lazers.Length; i++){
            lazers[i].bulletPrefab.transform.rotation = Quaternion.Euler(
                transform.eulerAngles + lazers[i].offsetRotation
            );
        }
    }
}
