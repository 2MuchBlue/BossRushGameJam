using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersShooter : MonoBehaviour
{
    public BulletPattern bullet;
    public float bpm = 150;
    public float shotsPerBeat = 0.25f;
    Vector3 aimVector;

    bool trying2shoot = false;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("tryShoot", 0, 60 / bpm * shotsPerBeat);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 aim = new Vector3(
            Input.GetAxisRaw("ShootHorz"),
            Input.GetAxisRaw("ShootVert"),
            0
        ).normalized;

        if(aim.magnitude > 0.1){
            float offset = 0;
            if(aim.x < 0){
                offset = 180;
            }
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan(aim.y/aim.x) * Mathf.Rad2Deg + offset);
            aimVector = aim;
            trying2shoot = true;
        }else{
            trying2shoot = false;
        }
        //angle = Mathf.Floor(Time.realtimeSinceStartup / 60 * bpm * shotsPerBeat);
    }

    void tryShoot(){
        //float messures = Time.realtimeSinceStartup / 60 * bpm;
        //float beats = messures * shotsPerBeat;
        if(trying2shoot){
            GameObject bulletGameObject = Instantiate(
                bullet.bulletPrefab, 
                transform.position + bullet.offsetPosition, 
                Quaternion.Euler(transform.eulerAngles + bullet.offsetRotation)
            );
            if(bulletGameObject.TryGetComponent<BasicBulletMovement>(out BasicBulletMovement bulletMovement)){
                bulletMovement.speed = bullet.speed;
            }
            if(bulletMovement.TryGetComponent<HurtOnHit>(out HurtOnHit hurter)){
                hurter.canHit = bullet.canHitMask;
                hurter.speed = bulletMovement.speed;
            }
        }
    }
}
