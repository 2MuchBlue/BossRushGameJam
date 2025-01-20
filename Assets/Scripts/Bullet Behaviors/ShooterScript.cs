using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShooterScript : MonoBehaviour
{
    [Header("Setup")]
    public bool autoShoot = true;
    
    [Header("number of times to fire in one minute")]
    public float unitPerMin = 60;
    public float unitMultiplyer = 1;
    public BulletPattern[] bulletPrefabs;

    [SerializeField]
    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("TryShoot", 0, 60 / unitPerMin * unitMultiplyer);
    }

    public void shoot(int index){
        if(bulletPrefabs[index].bulletPrefab == null){
            return;
        }

        GameObject bullet = Instantiate(
            bulletPrefabs[index].bulletPrefab, 
            transform.position + bulletPrefabs[index].offsetPosition, 
            Quaternion.Euler(transform.eulerAngles + bulletPrefabs[index].offsetRotation)
        );
        if(bullet.TryGetComponent<BasicBulletMovement>(out BasicBulletMovement bulletMovement)){
            bulletMovement.speed = bulletPrefabs[index].speed;
        }
        if(bullet.TryGetComponent<HurtOnHit>(out HurtOnHit hurter)){
            hurter.canHit = bulletPrefabs[index].canHitMask;
            hurter.speed = bulletPrefabs[index].speed;
        }
    }

    void TryShoot(){
        if(bulletPrefabs[index].bulletPrefab == null){
            /*index++;
            if(index > bulletPrefabs.Length - 1){
                index = 0;
            }
            */
            return;
        }

        GameObject bullet = Instantiate(
            bulletPrefabs[index].bulletPrefab, 
            transform.position + bulletPrefabs[index].offsetPosition, 
            Quaternion.Euler(transform.eulerAngles + bulletPrefabs[index].offsetRotation)
        );
        if(bullet.TryGetComponent<BasicBulletMovement>(out BasicBulletMovement bulletMovement)){
            bulletMovement.speed = bulletPrefabs[index].speed;
        }
        if(bullet.TryGetComponent<HurtOnHit>(out HurtOnHit hurter)){
            hurter.canHit = bulletPrefabs[index].canHitMask;
            hurter.speed = bulletPrefabs[index].speed;
        }
/*
        index++;
        if(index > bulletPrefabs.Length - 1){
            index = 0;
        }*/
    }

}