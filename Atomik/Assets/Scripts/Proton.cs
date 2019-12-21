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

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Particle otherParticle = contact.otherCollider.GetComponent<Particle>();
            if(otherParticle.m_Charge != Charge.Negative)
            {
                FixedJoint joint = gameObject.AddComponent(typeof(FixedJoint)) as FixedJoint;
                joint.connectedBody = otherParticle.gameObject.GetComponent<Rigidbody>();
            }
        }
    }

}
