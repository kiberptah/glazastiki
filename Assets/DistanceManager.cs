using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceManager : MonoBehaviour
{
    public GameObject Unit1;
    public GameObject Unit2;

    void Update()
    {
        if (Unit1 == null || Unit2 == null)
        {
            Destroy(gameObject);
        }
    }
}
