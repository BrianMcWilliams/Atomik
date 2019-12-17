using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proton : Particle
{
    Proton()
    {
        m_Charge = Charge.Positive;
        m_AccelerationDirection = new Vector3();
        m_Label = "P";
    }
}
