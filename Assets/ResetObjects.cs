using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


public class ResetObjects : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        InitialPosition grabComponent = other.GetComponent<InitialPosition>();
        if(grabComponent != null){
            other.transform.position = grabComponent.pos;
            other.transform.rotation = grabComponent.rot;
        }
    }
}
