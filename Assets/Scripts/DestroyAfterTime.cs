using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float timeTillDestroy = 5.0f;
    private void Start()
    {
        Destroy(gameObject, timeTillDestroy);
    }
}
