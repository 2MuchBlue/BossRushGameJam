using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RandomLazerAttack : MonoBehaviour
{
    globalSceneSettings globalSceneSettings;
    [SerializeField]
    bool useGlobalSettings = true;

    [Header("Lazer Attack Settings")]
    public float radiusFromCenter = 10;
    public int lazerCount = 5; // lazers to shoot before attack is over;
    public float shootCount = 4; // bullets shot on each call;
    public float bpm = 60; // to get the time between shots, get  minute per <beats> ( 60/ <beats> )
    public float masterShootMultiply = 0.25f;

    public GameObject lazerObject;
    int i = 0; // bullets shot already;

    [SerializeField]
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if(useGlobalSettings){
            globalSceneSettings = GameObject.FindGameObjectWithTag("globalSceneSettingsObject").GetComponent<globalSceneSettings>();
            bpm = globalSceneSettings.bpm;
        }

        if(transform.eulerAngles.z < 0){
            shootCount = transform.eulerAngles.z;
        }
        UnityEngine.Random.InitState(this.gameObject.scene.GetHashCode() * (int)Time.timeSinceLevelLoad);
        if (player == null){
            GameObject[] poliblePlayers = GameObject.FindGameObjectsWithTag("Player");
            
            if(poliblePlayers.Length < 1){
                Destroy(this.gameObject);
                return;
            }
            player = poliblePlayers[0];
            if(poliblePlayers.Length > 1){
                Debug.Log("There are multiple players! This can cause issues!!!!");
            }
        }

        float minsPerBeat = 60 / bpm;
        InvokeRepeating("ShootAttempt", 0, minsPerBeat);
    }

    void ShootAttempt(){

        randomPos();
        float minsPerBeat = 60 / bpm;
        for(int j = 1; j < shootCount + 1; j++ ){
            Invoke("Shoot", minsPerBeat * j * masterShootMultiply);
        }
        
        i++;
        if(i > lazerCount){
            Destroy(this.gameObject);
        }
    }

    void randomPos(){
        Vector2 randomPoint = UnityEngine.Random.insideUnitCircle;
        randomPoint = randomPoint.normalized;

        transform.position = randomPoint * radiusFromCenter;
        transform.position = new Vector3(
            transform.position.x,
            transform.position.y,
            0
        );

        transform.eulerAngles = new Vector3(0, 0, vecToAngle(transform.position - player.transform.position));
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
