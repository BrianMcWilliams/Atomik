using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlying : MonoBehaviour
{
    public float m_Speed;
    public GameObject m_CenterEyeAnchor;

    CharacterController m_Controller;
    Rigidbody m_Rigidbody;
    bool m_MovementEnabled;

    // Start is called before the first frame update
    void Start()
    {
        m_Controller = GetComponent<CharacterController>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_MovementEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        float primaryIndex = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float secondaryIndex = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        if(m_MovementEnabled)
        {
            if (primaryAxis.y >= 0.0f && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.2)
            {
                Vector3 velocity = m_Rigidbody.velocity;
                velocity.y = m_Speed * primaryAxis.y;
                m_Rigidbody.velocity = velocity;
            }
            else if (primaryAxis.y <= 0.0f && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.2)
            {
                Vector3 velocity = m_Rigidbody.velocity;
                velocity.y = m_Speed * primaryAxis.y;
                m_Rigidbody.velocity = velocity;
            }
            else if (primaryAxis.x != 0.0f || primaryAxis.y != 0.0f)
            {
                m_Rigidbody.velocity = m_CenterEyeAnchor.transform.forward * m_Speed * primaryAxis.y + m_CenterEyeAnchor.transform.right * m_Speed * primaryAxis.x;
            }
            else if (primaryAxis.x == 0.0f && primaryAxis.y == 0.0f) //stop movement when the joystick isn't being used
            {
                m_Rigidbody.velocity = m_CenterEyeAnchor.transform.forward * 0.0f + m_CenterEyeAnchor.transform.right * 0.0f;
            }
        }
    }

    public void SetMovementEnabled(bool value)
    {
        m_MovementEnabled = value;
    }
}
