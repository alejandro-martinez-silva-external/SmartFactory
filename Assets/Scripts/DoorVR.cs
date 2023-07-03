using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class DoorVR : XRBaseInteractable
{
    [SerializeField] private Transform m_Door = null;
    [SerializeField] private float m_DoorPosition = 0.0f;
    [SerializeField] private bool m_LockToPosition = false;
    private IXRSelectInteractor m_Interactor = null;
    private bool m_Grabbing = false;
    public UnityEvent<bool> OnDoorOpen = null;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        m_Interactor = args.interactor as IXRSelectInteractor;
        m_Interactor.selectExited.AddListener(EndGrab);
        m_Grabbing = true;
        Debug.Log("StartGrab");
    }

    private void EndGrab(SelectExitEventArgs args)
    {
        if (m_Grabbing)
        {
            if (m_LockToPosition)
            {
                float targetPosition = Mathf.Round(m_DoorPosition);
                SetDoorPosition(targetPosition, true);
            }
            print("EndGrab");
            m_Grabbing = false;
            m_Interactor.selectExited.RemoveListener(EndGrab);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        print("");
        EndGrab(args);
    }

    private void Update()
    {
        if (m_Grabbing)
        {
            Vector3 localGrabPoint = m_Door.InverseTransformPoint(m_Interactor.transform.position);
            float doorPosition = Mathf.Clamp01((localGrabPoint.x + 1.0f) * 0.5f);
            Debug.Log("UpdateGrab");
            SetDoorPosition(doorPosition, false);
        }
    }

    private void SetDoorPosition(float position, bool updateEvents)
    {
        m_DoorPosition = position;
        m_Door.localRotation = Quaternion.Euler(0.0f, -90.0f * position, 0.0f);

        if (updateEvents)
        {
            if (m_DoorPosition == 1.0f)
            {
                OnDoorOpen?.Invoke(true);
            }
            else
            {
                OnDoorOpen?.Invoke(false);
            }
        }
    }
}