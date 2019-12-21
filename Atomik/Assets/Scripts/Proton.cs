using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proton : Particle
{
    Proton()
    {
        m_Charge = Charge.Positive;
        m_AccelerationDirection = new Vector3();
        m_Speed = 0.0f;
        m_SpeedDirection = new Vector3(0, 0, 0);
        m_Label = "P";
    }
}
