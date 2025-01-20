using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingBullet : MonoBehaviour
{
    public float swingSpeed = 1; // full swings per second
    public float swingDistance = 10;
    public Vector3 orbitPoint = Vector3.zero;

    public bool turn = false;
    [SerializeField]
    float degs = 60; // degrees to turn per swing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float timeAngle = Time.realtimeSinceStartup * 2 * Mathf.PI;
        float dist = Mathf.Cos( timeAngle * swingSpeed) * swingDistance;
        transform.position = orbitPoint + ( dist * transform.right);
    }
}
