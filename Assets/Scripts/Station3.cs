using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
public class Station3 : MonoBehaviour
{
    public float detectRange;
    public float sizeMultiplier;
    public float validationTime;
    public float lightDuration;
    public Transform center;
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;
    public GameObject[] validObjects;
    private bool isValidating;
    private float validatingTimer;
    private StationMovableDoor movableDoor;
    private float lightTimer;

    public TextMeshProUGUI statusTxt;

    private void Awake() {
        movableDoor = FindObjectOfType<StationMovableDoor>();
    }

    void Start()
    {
        redLight.SetActive(false);
        yellowLight.SetActive(false);
        greenLight.SetActive(false);
    }

    void Update()
    {
        if(isValidating){
            validatingTimer += Time.deltaTime;
            if(validatingTimer >= validationTime){
                validatingTimer = 0;
                isValidating = false;
                yellowLight.SetActive(false);
                CheckItem();
            }
        }
        if(redLight.activeSelf || yellowLight.activeSelf || greenLight.activeSelf){
            lightTimer += Time.deltaTime;
            if(lightTimer >= lightDuration){
                redLight.SetActive(false);
                yellowLight.SetActive(false);
                greenLight.SetActive(false);
            }
        }
    }

    public void StartValidating(){
        isValidating = true;
        redLight.SetActive(false);
        greenLight.SetActive(false);
        yellowLight.SetActive(true);
        statusTxt.text = "Validating...";
    }

    public void PauseTimer(){
        isValidating = false;
        statusTxt.text = "Validation Paused";
    }

    public void StopTimer(){
        isValidating = false;
        lightTimer = 0;
        statusTxt.text = "Validation Stopped";
    }

    public void CheckItem(){
        if(validObjects.Length <= 0){
            return;
        }

        Collider[] colliders= Physics.OverlapBox(center.position, transform.localScale * sizeMultiplier, Quaternion.identity);
        if(colliders.Length <= 0) {
            redLight.SetActive(true);
            lightTimer = 0;

            return;
        }
        for(int i = 0; i < colliders.Length; i++){
            if(colliders[i].GetComponentInChildren<XRGrabInteractable>()){
                for(int j = 0; j < validObjects.Length; j++){
                    if(colliders[i].gameObject == validObjects[j]){
                        greenLight.SetActive(true);
                        statusTxt.text = "Correct Item";
                        lightTimer = 0;
                        movableDoor.InteractDoor();
                    }
                }
            }
        }
        if(!greenLight.activeSelf) {
            redLight.SetActive(true);
            statusTxt.text = "Wrong Item";
            lightTimer = 0;
            movableDoor.InteractDoor();
        }
        lightTimer = 0;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawCube(center.position, transform.localScale * sizeMultiplier);
        Gizmos.color = Color.red;
    }
}
