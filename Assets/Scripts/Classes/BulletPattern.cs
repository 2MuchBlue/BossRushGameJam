using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BulletPattern
{
    public GameObject bulletPrefab;
    public Vector3 offsetPosition;
    public Vector3 offsetRotation;
    public LayerMask canHitMask = -1;
    public float speed = 5;
}
