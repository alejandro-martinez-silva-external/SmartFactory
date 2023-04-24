using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class DoorInteractable : Interactable
{
    [SerializeField] private GameObject doorPivot;
    [SerializeField] private float openingAngle = 120f;
    [SerializeField] private string openDoorTxt;
    [SerializeField] private string closeDoorTxt;
    [SerializeField] private TextMeshProUGUI interactTxt01;

    [SerializeField] private TextMeshProUGUI interactTxt02;
    [SerializeField] bool useDiegeticTxt;

    new void Start()
    {
        base.Start();
        
        interactTxt01.gameObject.SetActive(false);
        interactTxt02.gameObject.SetActive(false);
        
        controllerActionGrip.action.started += CheckInput;

        if(isOpen){
            interactTxt.text = closeDoorTxt;
            interactTxt01.text = closeDoorTxt;
            interactTxt02.text = closeDoorTxt;
        }
        else{
            interactTxt.text = openDoorTxt;
            interactTxt01.text = openDoorTxt;
            interactTxt02.text = openDoorTxt;
        }
    }

    public void CheckInput(InputAction.CallbackContext obj){
        if(canBeUsed){
            if(isOpen){
               CloseDoor();
            }
            else{
               OpenDoor();
            }
        }
    }

    private void OpenDoor(){
        doorPivot.transform.Rotate(0, openingAngle, 0);
        isOpen = true;
        if(useDiegeticTxt){
            interactTxt01.text = closeDoorTxt;
            interactTxt02.text = closeDoorTxt;
        }
        if(use3DTxt)  interactTxt.text = closeDoorTxt;
    }

    private void CloseDoor(){
        doorPivot.transform.Rotate(0, -openingAngle, 0);
        if(useDiegeticTxt){
            interactTxt01.text = openDoorTxt;
            interactTxt02.text = openDoorTxt;
        }
        isOpen = false;
        if(use3DTxt)  interactTxt.text = openDoorTxt;
    }

    new void OnTriggerEnter(Collider other) {
        base.OnTriggerEnter(other);
        if(other.CompareTag("Player")){
            if(useDiegeticTxt){
                interactTxt01.gameObject.SetActive(true);
                interactTxt02.gameObject.SetActive(true);
            }
        }
    }

    new void OnTriggerExit(Collider other) {
        base.OnTriggerExit(other);
        if(other.CompareTag("Player")){
            if(useDiegeticTxt){
                interactTxt01.gameObject.SetActive(false);
                interactTxt02.gameObject.SetActive(false);
            }
        }
    }
}
