using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI interactTxt;
    [SerializeField] protected InputActionReference controllerActionGrip = null;
    [SerializeField] protected bool use3DTxt;

    
    public bool canBeUsed;
    public bool isOpen;

    // Start is called before the first frame update
   
    protected void Start() {
        interactTxt.gameObject.SetActive(false);
    }

    protected void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player")){
            if(use3DTxt){
                interactTxt.gameObject.SetActive(true);
            }
            canBeUsed = true;
        }
    }

    protected void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            if(use3DTxt){
                interactTxt.gameObject.SetActive(false);
            }
            canBeUsed = false;
        }
    }
}
