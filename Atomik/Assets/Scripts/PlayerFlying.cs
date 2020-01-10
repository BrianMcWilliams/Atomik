using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlying : MonoBehaviour
{
    public float m_Speed;
    public GameObject m_CenterEyeAnchor;

    Rigidbody m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);
        float primaryIndex = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger);
        float secondaryIndex = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        if (primaryAxis.x != 0.0f || primaryAxis.y != 0.0f)
        {
            m_Rigidbody.velocity = m_CenterEyeAnchor.transform.forward * m_Speed * primaryAxis.y + m_CenterEyeAnchor.transform.right * m_Speed * primaryAxis.x;
        }
        else if (primaryAxis.x == 0.0f && primaryAxis.y == 0.0f) //stop movement when the joystick isn't being used
        {
            m_Rigidbody.velocity = m_CenterEyeAnchor.transform.forward * 0.0f + m_CenterEyeAnchor.transform.right * 0.0f;
        }
  
    }

}
