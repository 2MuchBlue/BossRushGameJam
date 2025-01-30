using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour
{
    [SerializeField]
    float growSpeed = 5;

    // Update is called once per frame
    void Update()
    {
        transform.localScale += (new Vector3(1, 1, 1) - transform.localScale) * Time.deltaTime * growSpeed;
    }
}
