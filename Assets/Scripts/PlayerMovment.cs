using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [Range(0, 10)]
    public float speed = 1f;

    [Range(0, 20)]
    public float upSpeed = 1f;

    [Range(0, 20)]
    public float downSpeed = 1f;
    Vector3 velocity;

    float rollDist = 5;

    [SerializeField]
    TimerHolder rollTimer;

    CharacterController cc;

    [Header("Camera Control")]
    [SerializeField]
    Camera cam;
    [SerializeField]
    float advanceValue = 3;
    [SerializeField]
    float camDrag = 0.99f;
    [SerializeField]
    Vector3 cameraVelocity;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        movementVector = Vector3.ClampMagnitude(movementVector, 1);

        //Vector3 targetCamPos = transform.position + (movementVector * advanceValue);

        //targetCamPos.z = -10;

        float dragVal = upSpeed;
        /*if(movementVector.magnitude >= 0.1f){
            dragVal = upSpeed;
        }else{
            dragVal = downSpeed;
        }*/

        if(Input.GetButtonDown("Jump") && rollTimer.isFinished()){
            rollTimer.startTimer();
            cc.Move(movementVector * rollDist);
        }

        velocity += ((movementVector * speed * Time.deltaTime) - velocity) * Time.deltaTime * dragVal ;
        //cameraVelocity = (targetCamPos - cam.transform.position) * Time.deltaTime * camDrag;
        cc.Move(velocity);
        transform.position = new Vector3(
            transform.position.x, transform.position.y,
            0
        );
        //Debug.Log(cameraVelocity);
        //cam.transform.position += cameraVelocity;
    }
}
