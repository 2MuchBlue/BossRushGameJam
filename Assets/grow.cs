using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grow : MonoBehaviour
{
    [SerializeField]
    float growSpeed = 5;

    public Vector3[] targetScales = new Vector3[] { new Vector3(1, 1, 1) };

    public int targetIndex = 0;

    public void changeIndex (int index2change2 ){
        targetIndex = index2change2;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale += (targetScales[targetIndex] - transform.localScale) * Time.deltaTime * growSpeed;
    }
}
