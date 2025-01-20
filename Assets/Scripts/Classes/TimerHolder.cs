using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TimerHolder
{
    public float timeOfStart = 0;
    public float lifeTime = 0;

    public bool isFinished(){
        return timeOfStart + lifeTime < Time.realtimeSinceStartup;
    }

    public float timeLeft(){
        return Time.realtimeSinceStartup - timeOfStart;
    }

    public void startTimer(){
        timeOfStart = Time.realtimeSinceStartup;
    }
}
