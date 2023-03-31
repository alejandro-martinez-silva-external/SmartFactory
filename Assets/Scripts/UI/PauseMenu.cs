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


    private void Awake()
    {
        controllerActionGrip.action.started += ToggleCanvas;
    }

    void Start(){
    pausePanel.SetActive(false);
    settingsPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UpdatePauseMenu();
        }
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
}
