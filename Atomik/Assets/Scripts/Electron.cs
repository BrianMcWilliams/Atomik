using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electron : Particle
{
    Electron()
    {
        m_Charge = Charge.Negative;
        m_AccelerationDirection = new Vector3();
    }
}
