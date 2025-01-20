using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnTouch : MonoBehaviour
{
    [SerializeField]
    float damageOnTouch = 10;
    [SerializeField]
    LayerMask canHit;
    [SerializeField]
    float radius = -1;
    // Start is called before the first frame update
    void Start()
    {
        if(radius < 0){
            radius = Mathf.Max(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            if(TryGetComponent<SphereCollider>(out SphereCollider col)){
                radius += Mathf.Max(col.radius, radius );
            }
            radius += 0.05f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] toClose = Physics.OverlapSphere(transform.position, radius, canHit, QueryTriggerInteraction.Ignore);
        for(int i = 0; i < toClose.Length; i++){
            if(toClose[i].gameObject.TryGetComponent<HealthHadler>(out HealthHadler hadler)){
                hadler.Health -= damageOnTouch * Time.deltaTime;
                //Debug.Log("tried to hit handler");
            }
            //Debug.Log("try to hit");
        }
        //Debug.DrawRay(transform.position, Vector3.back, Color.green, 0.1f);
    }
}
