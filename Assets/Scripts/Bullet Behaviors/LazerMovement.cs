using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerMovement : MonoBehaviour
{
    public float speed = 1;
    public float damage = 10;
    public LayerMask canHit;
    public float radius = 0.2f;

    float startLife = 0;
    public float lifetime = 10;

    LineRenderer lazerRenderer;

    float range = 200;

    // Start is called before the first frame update
    void Start()
    {
        startLife = Time.realtimeSinceStartup;
        if(TryGetComponent<LineRenderer>(out LineRenderer lRen)){
            lazerRenderer = lRen;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(lazerRenderer != null){
            lazerRenderer.positionCount = 2;
            lazerRenderer.SetPosition(0, transform.position);
        }
        if(Physics.SphereCast(transform.position, radius, transform.right, out RaycastHit hit, range, canHit, QueryTriggerInteraction.Collide)){
            if(hit.collider.gameObject.TryGetComponent<HealthHadler>(out HealthHadler hadler)){
                hadler.Health -= damage * Time.deltaTime;
            }
            //Debug.Log("hitSomething");
            if(lazerRenderer != null){
                lazerRenderer.SetPosition(1, hit.point);
            }
        }else{
            if(lazerRenderer != null){
                lazerRenderer.SetPosition(1, transform.right * range);
            }
        }
        /*if(startLife + lifetime < Time.realtimeSinceStartup){
            bulletExpire();
        }*/
    }

    void bulletExpire(){
        Destroy(this.gameObject);
    }
}
