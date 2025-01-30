using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class AttackScheduler : MonoBehaviour
{
    public Animator animatorControl;

    [TextArea]
    public string attackOrder;
    int[] attackInts;
    int index;

    public CanTriggerAttackClass[] Attacks;
    public UnityEvent OnBossKilled;
    int thingsStillUp;
    
    // Start is called before the first frame update
    void Start()
    {
        thingsStillUp = Attacks.Length;
        attackInts = System.Array.ConvertAll(attackOrder.Split(','), int.Parse);
    }
    /*
    // Update is called once per frame
    void Update()
    {
        
     //   animatorControl.SetTrigger("Attack 1");
    }*/

    public void chooseNextAttack(int attempts = 0){
        if(thingsStillUp < 1){
            OnBossKilled.Invoke();
            return;
        }
        if(attempts > Attacks.Length){
            return;
        }
        CanTriggerAttackClass attack = Attacks[attackInts[index % attackInts.Length] % Attacks.Length];
        if(attack.canBeTriggered){
            animatorControl.SetTrigger(attack.trigger);
            Debug.Log("chosen index: " + index);
            index++;
            return;
        }else{
            index++;
            attempts++;
            chooseNextAttack(attempts);
            return;
        }
    }

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
        thingsStillUp--;
    }
}
