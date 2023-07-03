using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SnapObject : MonoBehaviour
{
    public bool isUsed = false;
    XRGrabInteractable currComp = null;

    private void OnTriggerEnter(Collider other) {
        XRGrabInteractable grabComp = other.GetComponent<XRGrabInteractable>();
        
        if(grabComp != null){
            if(!isUsed){
                isUsed = true;
                other.transform.position = transform.position;
                other.transform.rotation = transform.rotation;
            }
            else{
                InitialPosition initialPos = currComp.gameObject.GetComponent<InitialPosition>();
                currComp.gameObject.transform.position = initialPos.pos;
                currComp.gameObject.transform.rotation = initialPos.rot;
                other.transform.position = transform.position;
                other.transform.rotation = transform.rotation;
                isUsed = true;
            }
            currComp = grabComp;
            currComp.GetComponent<Rigidbody>().velocity = Vector3.zero;
            grabComp.enabled = false;
            grabComp.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        XRGrabInteractable grabComp = other.GetComponent<XRGrabInteractable>();
        
        if(grabComp != null){
            if(grabComp == currComp)
            isUsed = false;
        }
    }
}
