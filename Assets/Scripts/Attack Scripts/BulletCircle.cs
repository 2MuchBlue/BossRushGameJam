using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCircle : MonoBehaviour
{
    globalSceneSettings globalSceneSettings;
    [SerializeField]
    bool useGlobalSettings = true;

    [Header("Play With These")]
    public bool rippleBullets = false; // if true, bullets will fire in the order they were placed
    public float radiusFromCenter = 10;
    public int bulletCount = 5; // lazers to shoot before attack is over;
    public float timeToMakeFullCircle = 5; // in seconds

    public GameObject lazerObject;

    [Space]
    public GameObject displayObject;
    [HideInInspector]
    public Vector3 center = Vector3.zero;
    [HideInInspector]
    public int i = 0; // bullets set already;
    

    [HideInInspector]
    public GameObject[] bulletObjects;

    [HideInInspector]
    public float angle;

    // Start is called before the first frame update
    public void Start()
    {
        if(useGlobalSettings){
            globalSceneSettings = GameObject.FindGameObjectWithTag("globalSceneSettingsObject").GetComponent<globalSceneSettings>();
            timeToMakeFullCircle = 60 / globalSceneSettings.bpm;
        }

        displayObject.transform.SetParent(null, true);
        angle = transform.rotation.eulerAngles.z;
        bulletObjects = new GameObject[bulletCount];
        float tick = timeToMakeFullCircle / bulletCount;
        for(int j = 0; j < bulletCount; j++ ){
            Invoke("PlaceBullet", tick * j);
        }

        if(!rippleBullets){
            Invoke("Shoot", timeToMakeFullCircle);
        }
    }

    public void PlaceBullet(){
        float angleTicks = (2 * Mathf.PI) / bulletCount;
        
        transform.position = new Vector3(
            Mathf.Cos(angleTicks * i + (angle * Mathf.Deg2Rad)) * radiusFromCenter,
            Mathf.Sin(angleTicks * i + (angle * Mathf.Deg2Rad)) * radiusFromCenter,
            0
        );

        transform.eulerAngles = new Vector3(0, 0, vecToAngle(transform.position));

        bulletObjects[i] = Instantiate(lazerObject, transform.position, transform.rotation);
        i++;
    }

    public float vecToAngle(Vector3 vec){
        vec = Vector3.Normalize(vec);
        float offset = 0;
        if(vec.x < 0){
            offset = 180;
        }
        return Mathf.Atan(vec.y/vec.x) * Mathf.Rad2Deg + offset + 180;
    }

    public void Shoot(){
        for(int j = 0; j < bulletObjects.Length; j++ ){
            if(bulletObjects[j].TryGetComponent<BulletCircle_BulletBehavior>(out BulletCircle_BulletBehavior bBehavior)){
                bBehavior.Go();
                //Debug.Log("trying to shoot!");
            }
        }
        if(displayObject != null){
            Destroy(displayObject);
        }
        Destroy(this.gameObject);
    }
    
}
