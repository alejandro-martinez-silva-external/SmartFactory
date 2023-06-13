using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPosition : MonoBehaviour
{
    [HideInInspector] public Vector3 pos;
    [HideInInspector] public Quaternion rot;

    void Start()
    {
        pos = transform.position;
        rot = transform.rotation;        
    }

    public void Reset(){
        transform.position = pos;
        transform.rotation = rot;
    }
}
