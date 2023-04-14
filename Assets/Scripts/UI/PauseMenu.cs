using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;
using UnityEngine.InputSystem;


public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject settingsPanel;
    
    [SerializeField] GameObject leftReticle;
    [SerializeField] GameObject rightReticle;
    public Transform player;
    public float maxDistance = 10.0f;
    public float maxHeight;
    public float rotationSpeed = 0.1f;
    [SerializeField] private GameObject tunnelingEffect;

    [SerializeField] InputActionReference controllerActionGrip = null;

    private MeshRenderer tempMesh; // Aquí almacenaremos el mesh renderer que estorbe para el menu, y cambiaremos su visiblidad 
    private BoxCollider canvasColl;
    
    private void Awake()
    {
        controllerActionGrip.action.started += ToggleCanvas;

        canvasColl = GetComponent<BoxCollider>();
        if(canvasColl == null){
            Debug.LogWarning("BoxCollider en el Canvas no encontrado. Creando uno nuevo.");
            canvasColl = gameObject.AddComponent<BoxCollider>();
            canvasColl.size = new Vector3(85000, 475, 500);
            canvasColl.center = new Vector3(x: 165, 0, -7);
        }
    }

    void Start(){
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        canvasColl.enabled = false;
    }



    void LateUpdate(){
        // Mueve el Canvas al centro de la pantalla
        Vector3 centerScreen = new Vector3(Screen.width / 2, Screen.height / 2, maxDistance);
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(centerScreen);
        
        if (targetPos.y < maxHeight)
        {
            targetPos.y = maxHeight;
        }

        transform.position = targetPos;

        // Haz que el Canvas mire hacia el jugador
        Vector3 targetPosition = player.position + Vector3.up;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(transform.position - targetPosition), rotationSpeed);
    }

    void UpdatePauseMenu(){
        pausePanel.SetActive(!pausePanel.activeSelf);
        settingsPanel.SetActive(false);
        canvasColl.enabled = !canvasColl.enabled;

        if(tempMesh != null){
            // Color newColor = new Color(tempMesh.material.color.r, tempMesh.material.color.g, tempMesh.material.color.b, 1);
            // tempMesh?.material?.SetColor("_Color", newColor);
            // tempMesh = null;
            tempMesh.enabled = true;
        }
   
        //leftReticle.SetActive(!leftReticle.activeSelf);
        //rightReticle.SetActive(!rightReticle.activeSelf);
    }


    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void BackToGame()
    {
        UpdatePauseMenu();
    }

    public void UpdateTunneling(){
        tunnelingEffect.SetActive(!tunnelingEffect.activeSelf);
    }

    private void ToggleCanvas(InputAction.CallbackContext obj)
    {
        UpdatePauseMenu();
    }

    private void OnTriggerEnter(Collider other)
    {
        Collider tempColl = other;
        if(tempColl != null){
            if(!other.CompareTag("Floor")){
                tempMesh = other.GetComponent<MeshRenderer>();
                if(tempMesh != null){
                    // Color newColor = new Color(tempMesh.material.color.r, tempMesh.material.color.g, tempMesh.material.color.b, 0.5f);
                    // tempMesh?.material?.SetColor("_Color", newColor);
                    tempMesh.enabled = false;
                }
            }
        }
    } 

    private void OnTriggerExit(Collider other) {
        Collider tempColl = other;
        if(tempColl != null){
            if(!other.CompareTag("Floor") && other.GetComponent<MeshRenderer>() == tempMesh){
                if(tempMesh != null){
                    // Color newColor = new Color(tempMesh.material.color.r, tempMesh.material.color.g, tempMesh.material.color.b, 1);
                    // tempMesh?.material?.SetColor("_Color", newColor);
                    tempMesh.enabled = true;
                    print("low");
                    tempMesh = null;
                }
            }
        }
    }
}
