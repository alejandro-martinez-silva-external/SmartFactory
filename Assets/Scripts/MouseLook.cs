using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
    public InputActionReference horizontalLook;
    public InputActionReference verticalLook;
    public float lookSpeed = 1f;
    public Transform cameraTransform;
    float pitch;
    float yaw;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        verticalLook.action.performed += HandleHorizontalLookChange;
        horizontalLook.action.performed += HandleVerticalLookChange;
    }

    void HandleVerticalLookChange(InputAction.CallbackContext obj)
    {
        yaw += obj.ReadValue<float>();
        transform.localRotation = Quaternion.AngleAxis(yaw * lookSpeed, Vector3.up);
    }

    void HandleHorizontalLookChange(InputAction.CallbackContext obj)
    {
        pitch -= obj.ReadValue<float>();
        cameraTransform.localRotation = Quaternion.AngleAxis(pitch * lookSpeed, Vector3.right);
    }
}
