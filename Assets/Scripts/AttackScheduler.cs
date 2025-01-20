using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackScheduler : MonoBehaviour
{
    public Animator animatorControl;

    public CanTriggerAttackClass[] Attacks;
    /*
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
     //   animatorControl.SetTrigger("Attack 1");
    }*/

    public void pickRandomAttack(){
        int index = (int)Mathf.Round(UnityEngine.Random.Range(0, Attacks.Length));
        Debug.Log("starting index: " + index);
        
        for(int i = 0; i < Attacks.Length; i++ ){
            CanTriggerAttackClass attack = Attacks[(index + i) % Attacks.Length];
            if(attack.canBeTriggered){
                animatorControl.SetTrigger(attack.trigger);
                Debug.Log("chosen index: " + (index + i));
                return;
            }
        }
    }

    void tryAttack(int index, int timesTryed = 0){
        if(!Attacks[index % Attacks.Length].canBeTriggered && timesTryed < Attacks.Length){
            tryAttack((index + 1) % Attacks.Length, timesTryed + 1);
        }
        if(timesTryed > Attacks.Length){
            Destroy(this);
        }
        animatorControl.SetTrigger(Attacks[index % Attacks.Length].trigger);
    }

    public void disableAnAttack(int index){
        Attacks[index].canBeTriggered = false;
    }
}
