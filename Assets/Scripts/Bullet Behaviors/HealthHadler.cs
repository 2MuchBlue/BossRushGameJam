using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthHadler : MonoBehaviour
{
    public float MaxHealth = 100;
    public float Health = 100;

    [SerializeField]
    GameObject healthDisplayPrefab;
    [SerializeField]
    [Range(0, 5)]
    float radius = 0.5f;
    GameObject healthDisplay;
    Material healthDisplayMat;

    [SerializeField]
    UnityEvent onDeath;

    // Start is called before the first frame update
    void Start()
    {
        if(healthDisplayPrefab != null){
            healthDisplay = Instantiate(healthDisplayPrefab, transform.position + new Vector3(0, 0, 1), transform.rotation, transform);
            healthDisplay.transform.localScale = new Vector3( 2 * radius, 2 * radius, 2 * radius);
            healthDisplayMat = new Material(Shader.Find("Unlit/CircleDisplay"));
            healthDisplay.GetComponent<Renderer>().material = healthDisplayMat;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(healthDisplayMat != null){
            healthDisplayMat.SetFloat("_Progress", 1 - Health / MaxHealth);
        }

        if(Health <= 0){
            killThis();
        }
    }

    void killThis(){
        if(onDeath != null){
            onDeath.Invoke();
        }
        gameObject.SetActive(false);
    }
}
