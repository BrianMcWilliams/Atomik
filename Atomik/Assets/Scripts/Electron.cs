using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : Particle
{
    Electron()
    {
        m_Charge = Charge.Negative;
        m_AccelerationDirection = new Vector3();
        m_Speed = 0.0f;
        m_SpeedDirection = new Vector3(0, 0, 0);
        m_Label = "E";
    }
}
