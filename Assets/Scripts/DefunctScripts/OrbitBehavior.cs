using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitBehavior : MonoBehaviour
{

    [SerializeField, Header("BPM (used in bpm unit)")]
    float rpm = 150;

    [SerializeField, Header("Rotates this many times per beat (when using <bpm>) or speed multiplyer for <arbatrary> time")]
    float rotationsPerUnit = 1;

    [SerializeField]
    List<GameObject> objects;
    [SerializeField]
    List<Vector3> orbitalOffsets;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void addObjectToOrbit(GameObject orbitObject){
        objects.Add(orbitObject);
        orbitalOffsets.Add(orbitObject.transform.position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float angle = Time.realtimeSinceStartup / 60 * rpm * rotationsPerUnit * 2 * Mathf.PI;
        
        for(int i = 0; i < objects.Count; i++){
            setOrbitPos(objects[i], orbitalOffsets[i], angle);
        }
    }

    void setOrbitPos(GameObject orbitObject, Vector3 offset, float angle){
        Vector2 rotatedPoint = rotate(new Vector2(offset.x, offset.y), angle);
        Vector3 target = new Vector3(
            rotatedPoint.x,
            rotatedPoint.y,
            0
        ) + transform.position;
        orbitObject.transform.position = target;
    }

    Vector2 rotate(Vector2 point2rot, float angle){
        return new Vector2(
            point2rot.x * Mathf.Cos(angle) + point2rot.y * Mathf.Sin(angle),
            point2rot.x * -Mathf.Sin(angle) + point2rot.y * Mathf.Cos(angle)
        );
    }
}
