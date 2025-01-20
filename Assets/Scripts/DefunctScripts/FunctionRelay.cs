using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FunctionRelay : MonoBehaviour
{
    [SerializeField]
    public UnityEvent[] functions;
    
    public void RunMethod(int index){
        functions[index].Invoke();
    }
}
