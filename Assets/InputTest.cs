using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    [SerializeField] InputActionReference controllerActionGrip = null;
    

    private void Awake(){
        controllerActionGrip.action.started += Toggle;
    }

    private void Update()
    {
        // Botón A
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("Se presionó el botón A");
        }

        // Botón B
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            Debug.Log("Se presionó el botón B");
        }

        // Botón X
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            Debug.Log("Se presionó el botón X");
        }

        // Botón Y
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            Debug.Log("Se presionó el botón Y");
        }

        // Botón de agarre
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
        {
            Debug.Log("Se presionó el botón de agarre");
        }

        // Botón de menú
        if (OVRInput.GetDown(OVRInput.Button.Start))
        {
            Debug.Log("Se presionó el botón de menú");
        }

        // Botón de volver
        if (OVRInput.GetDown(OVRInput.Button.Back))
        {
            Debug.Log("Se presionó el botón de volver");
        }

        // Gatillo
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            Debug.Log("Se presionó el gatillo");
        }

        // Joystick izquierdo
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
        {
            Debug.Log("Se presionó el joystick izquierdo");
        }

        // Joystick derecho
        if (OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            Debug.Log("Se presionó el joystick derecho");
        }

        // Gatillo derecho
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("Se presionó el gatillo derecho");
        }

        // Botón de agarre derecho
        if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
        {
            Debug.Log("Se presionó el botón de agarre derecho");
        }
    }

    private void Toggle(InputAction.CallbackContext obj){
        Debug.Log("Test");
    }

    private void OnDestroy(){
        controllerActionGrip.action.started -= Toggle;
    }
}