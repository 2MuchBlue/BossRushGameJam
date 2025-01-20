using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlyby : BulletCircle
{
    new public void PlaceBullet(){
        float angleTicks = (2 * Mathf.PI) / bulletCount;
        
        transform.position = new Vector3(
            Mathf.Cos(angleTicks * i + (angle * Mathf.Deg2Rad)) * radiusFromCenter,
            Mathf.Sin(angleTicks * i + (angle * Mathf.Deg2Rad)) * radiusFromCenter,
            0
        );

        transform.eulerAngles = new Vector3(0, 0, vecToAngle(transform.position) + angle);

        bulletObjects[i] = Instantiate(lazerObject, transform.position, transform.rotation);
        i++;
    }
}
