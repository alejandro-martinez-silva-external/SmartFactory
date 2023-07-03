using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class WindowInteractable : Interactable
{
    [SerializeField] private GameObject windowPivot;
    [SerializeField] private float amountToMove;
    [SerializeField] private string openWindowTxt;
    [SerializeField] private string closeWindowTxt;
    [SerializeField] private TextMeshProUGUI interactTxtDieg;
    [SerializeField] bool useDiegeticTxt;

    new void Start()
    {
        base.Start();
        
        interactTxtDieg.gameObject.SetActive(false);
        
        controllerActionGrip.action.started += CheckInput;

        if(isOpen){
            interactTxt.text = closeWindowTxt;
            interactTxtDieg.text = closeWindowTxt;
        }
        else{
            interactTxt.text = openWindowTxt;
            interactTxtDieg.text = openWindowTxt;
        }
    }

    public void CheckInput(InputAction.CallbackContext obj){
        if(canBeUsed){
            if(isOpen){
               CloseWindow();
            }
            else{
               OpenWindow();
            }
        }
    }

    private void OpenWindow(){
        windowPivot.transform.Translate(0, amountToMove, 0);
        isOpen = true;
        if(useDiegeticTxt){
            interactTxtDieg.text = closeWindowTxt;
        }
        if(use3DTxt)  interactTxt.text = closeWindowTxt;
    }

    private void CloseWindow(){
        windowPivot.transform.Translate(0, -amountToMove, 0);
        if(useDiegeticTxt){
            interactTxtDieg.text = openWindowTxt;
        }
        isOpen = false;
        if(use3DTxt)  interactTxt.text = openWindowTxt;
    }

    new void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        if(other.CompareTag("Player")){
            if(useDiegeticTxt){
                interactTxtDieg.gameObject.SetActive(true);
            }
        }
    }

    new void OnTriggerExit(Collider other) {
        base.OnTriggerExit(other);
        if(other.CompareTag("Player")){
            if(useDiegeticTxt){
                interactTxtDieg.gameObject.SetActive(false);
            }
        }
    }
}
