using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutron : Particle
{
    Neutron()
    {
        m_Charge = Charge.Neutral;
        m_AccelerationDirection = new Vector3(0,0,0); //This shouldn't ever change, neutrons don't get pushed/pulled in our simulation
        m_Speed = 0.0f;
        m_SpeedDirection = new Vector3(0,0,0);
        m_Label = "N";
    }
}
