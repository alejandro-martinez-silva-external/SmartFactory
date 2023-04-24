using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;

namespace UnityEngine.XR.Content.Interaction
{
public class DoorVRInteractable : MonoBehaviour
{
[SerializeField]
HingeJoint m_DoorJoint;
[SerializeField]
    [Tooltip("Transform joint that pulls a door to follow an interactor")]
    TransformJoint m_DoorPuller;

    [SerializeField]
    float m_HandleOpenValue = 0.1f;

    [SerializeField]
    float m_HingeCloseAngle = 5.0f;

    [SerializeField]
    float m_HandleCloseValue = 0.5f;

    JointLimits m_OpenDoorLimits;
    JointLimits m_ClosedDoorLimits;
    bool m_Closed = false;
    float m_LastHandleValue = 1.0f;

    [SerializeField]
    [Tooltip("Events to fire when the door is opened.")]
    UnityEvent m_OnOpen = new UnityEvent();

    [SerializeField]
    [Tooltip("Events to fire when the door is closed.")]
    UnityEvent m_OnClose = new UnityEvent();

    /// <summary>
    /// Events to fire when the door is opened.
    /// </summary>
    public UnityEvent onOpen => m_OnOpen;

    /// <summary>
    /// Events to fire when the door is closed.
    /// </summary>
    public UnityEvent onClose => m_OnClose;

    void Start()
    {
        m_OpenDoorLimits = m_DoorJoint.limits;
        m_ClosedDoorLimits = m_OpenDoorLimits;
        m_ClosedDoorLimits.min = 0.0f;
        m_ClosedDoorLimits.max = 0.0f;
        m_DoorJoint.limits = m_ClosedDoorLimits;
        m_Closed = true;
    }

    void Update()
    {
        // If the door is open, keep track of the hinge joint and see if it enters a state where it should close again
        if (!m_Closed)
        {
            if (m_LastHandleValue < m_HandleOpenValue)
                return;

            if (Mathf.Abs(m_DoorJoint.angle) < m_HingeCloseAngle)
            {
                m_DoorJoint.limits = m_ClosedDoorLimits;
                m_Closed = true;
                m_OnClose.Invoke();
            }
        }
    }

    public void BeginDoorPulling(SelectEnterEventArgs args)
    {
        m_DoorPuller.connectedBody = args.interactorObject.GetAttachTransform(args.interactableObject);
        m_DoorPuller.enabled = true;
    }

    public void EndDoorPulling()
    {
        m_DoorPuller.enabled = false;
        m_DoorPuller.connectedBody = null;
    }

    public void DoorHandleUpdate(float handleValue)
{
    if (handleValue < m_HandleOpenValue)
    {
        m_DoorJoint.limits = m_OpenDoorLimits;
        m_Closed = false;
    }
    else if (handleValue > m_HandleCloseValue)
    {
        m_DoorJoint.limits = m_ClosedDoorLimits;
        m_Closed = true;
    }
}
}
}