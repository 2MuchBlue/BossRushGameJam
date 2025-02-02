using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Subsystems;

public class BombBulletBehavior : MonoBehaviour
{
    globalSceneSettings globalSceneSettings;
    [SerializeField]
    bool useGlobalSettings = true;

    [Header("Bomb Bullet Settings")]
    public float beatsTillExplode = 4;
    float startTime;
    Vector3 startPos;
    public float timeTillExplode = 0.5f;
    [Range(0, 10)]
    public float distance = 5;

    public UnityEvent OnExplode;

    [SerializeField]
    float momentum;

    bool exploded = false;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.timeSinceLevelLoad;
        startPos = transform.position;
        if(useGlobalSettings){
            globalSceneSettings = GameObject.FindGameObjectWithTag("globalSceneSettingsObject").GetComponent<globalSceneSettings>();

            timeTillExplode = 60 / globalSceneSettings.bpm * beatsTillExplode;
        }
        Invoke("explodeBomb", timeTillExplode);
    }

    // Update is called once per frame
    void Update()
    {
        if(!exploded){
            float t = (Time.timeSinceLevelLoad - startTime) / timeTillExplode;
            momentum = Mathf.Sin(t * Mathf.PI * 0.5f) * distance;
            transform.position = startPos + transform.right * momentum;
        }
        if(momentum < 0f){
            explodeBomb();
        }

        if(Vector3.Distance(transform.position, Vector3.zero) > 45){
            Destroy(this.gameObject);
        }
    }

    public void explodeBomb(){
        
        Debug.Log("trying to explode");
        if(!exploded){
            OnExplode.Invoke();
        }
        exploded = true;
        //Destroy(this.gameObject);
    }

    public void waitToDestroy(int howLongToWait){
        transform.position = new Vector3(0, 0, 30);
        exploded = true;
        Invoke("destroyThisObject", howLongToWait);
        this.enabled = false;
    }

    void destroyThisObject(){
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision other){
        if(other.collider.CompareTag("Player")){
            explodeBomb();
        }
    }
}
