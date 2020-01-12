using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlying : MonoBehaviour
{
    public float m_Speed;
    public float RotationRatchet = 45.0f;
    public GameObject m_CenterEyeAnchor;
    public bool SnapRotation = true;

    private bool ReadyToSnapTurn;
    private float SimulationRate = 60f;
    //The rate of rotation when using a gamepad.
    public float RotationAmount = 1.5f;
    private float RotationScaleMultiplier = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
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
            Vector3 movement = m_CenterEyeAnchor.transform.forward * m_Speed * primaryAxis.y + m_CenterEyeAnchor.transform.right * m_Speed * primaryAxis.x;
            transform.Translate(movement * Time.deltaTime, Space.World);
        }

        //handle snap rotation using the right joystick.
        Vector3 euler = transform.rotation.eulerAngles;
        float rotateInfluence = SimulationRate * Time.deltaTime * RotationAmount * RotationScaleMultiplier;
        if (SnapRotation)
        {
            if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickLeft))
            {
                if (ReadyToSnapTurn)
                {
                    euler.y -= RotationRatchet;
                    ReadyToSnapTurn = false;
                }
            }
            else if (OVRInput.Get(OVRInput.Button.SecondaryThumbstickRight))
            {
                if (ReadyToSnapTurn)
                {
                    euler.y += RotationRatchet;
                    ReadyToSnapTurn = false;
                }
            }
            else
            {
                ReadyToSnapTurn = true;
            }
        }
        else
        {
            euler.y += secondaryAxis.x * rotateInfluence;
        }

        transform.rotation = Quaternion.Euler(euler);
    }

}
