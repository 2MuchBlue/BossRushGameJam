using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SummonAttacks : MonoBehaviour
{
    public GameObject bulletCircleAttackPrefab;
    public GameObject randomLazerAttackPrefab;
    public GameObject bulletFlyByAttackPrefab;
    public GameObject bulletAreaAttackPrefab;
    public GameObject ShootOnceAttackPrefab;
    public GameObject BombLauncherAttackPrefab;
    [Space]
    [Header("Audio")]
    public AudioSource[] audios;


    public void spawnBulletCircle(float angle){
        Instantiate(bulletCircleAttackPrefab, new Vector3(0, 0, 10), Quaternion.Euler(0, 0, angle));
    }

    public void spawnRandomLazerAttack(float attackCount = -1){
        Instantiate(randomLazerAttackPrefab, new Vector3(0, 0, 10), Quaternion.Euler(0, 0, attackCount));
    }
    public void bulletFlyByAttack(float angle = 45){
        Instantiate(bulletFlyByAttackPrefab, new Vector3(0, 0, 10), Quaternion.Euler(0, 0, angle));
    }

    public void bulletAreaAttack(int index = 0){
        GameObject bulletAttack = Instantiate(bulletFlyByAttackPrefab, new Vector3(0, 0, 10), Quaternion.Euler(0, 0, 0));
    }

    public void ShootOnceAttack(float angle = 0){
        Instantiate(ShootOnceAttackPrefab, new Vector3(0, 0, 10), Quaternion.Euler(0, 0, angle));
    }

    public void BombLauncherAttack(float angle = 0){
        Instantiate(BombLauncherAttackPrefab, new Vector3(0, 0, 10), Quaternion.Euler(0, 0, angle));
    }

    public void doNothing(){
        // this is to make the animator stop complaining. This function is empty :P
    }

    public void PlaySound(int index){
        audios[index].Play();
    }
}
