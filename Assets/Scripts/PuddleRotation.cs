using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleRotation : MonoBehaviour
{
    void Start()
    {
        gameObject.transform.eulerAngles = new Vector3(0,90 * Random.Range(0, 3), 0);
    }
}
