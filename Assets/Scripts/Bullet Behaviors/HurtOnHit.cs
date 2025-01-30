using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class HurtOnHit : MonoBehaviour
{
    public float speed = 1;
    public float lengthOfBullet = 0; // the length of the bullet so you don't miss if something ran into the back
    public float damage = 10;
    public LayerMask canHit;
    public float radius = 0.2f;

    float startLife = 0;
    public float lifetime = 10;

    public UnityEvent OnHitSomething;

    // Start is called before the first frame update
    void Start()
    {
        startLife = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapCapsule(transform.position - (transform.right * lengthOfBullet * 0.5f), transform.position + (transform.right * lengthOfBullet * 0.5f), radius, canHit, QueryTriggerInteraction.Ignore);
        if(hitColliders.Length > 0){
            for(int i = 0 ; i < hitColliders.Length; i++){
                if(hitColliders[i].gameObject.TryGetComponent<HealthHadler>(out HealthHadler hadler)){
                    hadler.Health -= damage;
                    //this.gameObject.name = "yo dog, I just hit a random guy";
                    //Debug.Log("found healthHandler, I am this object: " + this.gameObject);
                    bulletExpire();
                    return;
                }
            }
            
            //Debug.Log("hit something!");
        }

        if(Vector3.Distance(transform.position, Vector3.zero) > 45){
            bulletExpire();
        }
    }

    void bulletExpire(){
        OnHitSomething.Invoke();
        Destroy(this.gameObject);
    }
}
