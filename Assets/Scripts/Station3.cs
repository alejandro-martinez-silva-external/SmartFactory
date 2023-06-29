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
    public Animator armAnim;
    public TransparencyController transparencyController;
    public Transform center;
    public GameObject redLight;
    public GameObject yellowLight;
    public GameObject greenLight;
    public GameObject[] validObjects;
    private bool isValidating;
    private float validatingTimer;
    private StationMovableDoor movableDoor;
    private float lightTimer;
    private float textTimer;
    public float textTime2Disappear;
    private bool isTextDisplayed;

    public TextMeshProUGUI statusTxt;
    private TextValues textValues;

    private void Awake() {
        movableDoor = FindObjectOfType<StationMovableDoor>();
    }

    void Start()
    {
        textValues = new TextValues();

        SetText(false);
        statusTxt.color = Color.black;
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
        if(isTextDisplayed){
            textTimer += Time.deltaTime;
            if(textTimer >= textTime2Disappear){
                isTextDisplayed = false;
                SetText(false);
            }
        }
    }

    public void StartValidating(){
        if(isValidating){
            return;
        }
        armAnim.SetTrigger("scan");
        isValidating = true;
        redLight.SetActive(false);
        greenLight.SetActive(false);
        yellowLight.SetActive(true);
        SetText(true);
        statusTxt.color = Color.black;
        statusTxt.text = textValues.validationInProgress;
        transparencyController.StartSettingTransparency(2);
    }

    public void PauseTimer(){
        print("PauseTimer");
        if(lightTimer == 0) return;
        isValidating = !isValidating;
        statusTxt.color = Color.yellow;
        SetText(true);
        if(isValidating){
            statusTxt.text = textValues.validationResumed;
            print(textValues.validationResumed);
            armAnim.speed = 1;
        }
        else{
            statusTxt.text = textValues.validationPaused;
            print(textValues.validationPaused);
            armAnim.speed = 0;
        }
        
    }

    public void StopTimer(){
        if(!isValidating) return;
        isValidating = false;
        lightTimer = 0;
        statusTxt.color = Color.red;
        armAnim.SetTrigger("finish");
        SetText(true);
        print(textValues.validationStopped);
        statusTxt.text = textValues.validationStopped;
    }

    public void CheckItem(){
        if(validObjects.Length <= 0){
            return;
        }

        Collider[] colliders= Physics.OverlapBox(center.position, transform.localScale * sizeMultiplier, Quaternion.identity);
        string tempText = "";
        for(int i = 0; i < colliders.Length; i++){
            if(colliders[i].GetComponentInChildren<XRGrabInteractable>()){
                for(int j = 0; j < validObjects.Length; j++){
                    if(colliders[i].gameObject == validObjects[j]){
                        greenLight.SetActive(true);
                        tempText = textValues.correctItem;
                        statusTxt.color = Color.green;
                    }
                    else{
                        if(!greenLight.activeSelf) {
                            redLight.SetActive(true);
                            SetText(true);
                            tempText = textValues.wrongItem;
                            print(colliders[0].gameObject.transform.parent.name);
                            statusTxt.color = Color.red;
                            lightTimer = 0;
                        }
                    }
                }
            }
            else{
                if(tempText != textValues.correctItem && tempText != textValues.wrongItem){
                    tempText = textValues.noItemDetected;
                    statusTxt.color = Color.red;
                    redLight.SetActive(true);
                }
            }
        }
        
        
        SetText(true);
        statusTxt.SetText(tempText);
        lightTimer = 0;
        transparencyController.StartSettingTransparency(0);
    }

    public void SetText(bool val){
        // print("Val is: " + val);
        statusTxt.gameObject.SetActive(val);
        if(val){
            textTimer = 0;
            isTextDisplayed = true;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.DrawCube(center.position, transform.localScale * sizeMultiplier);
        Gizmos.color = Color.red;
    }
}
