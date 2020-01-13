using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Charge
{
    Positive,
    Negative,
    Neutral,
    INVALID_UNSET,
};
public class Particle : MonoBehaviour
{
    public Charge m_Charge = Charge.INVALID_UNSET;
    public Vector3 m_AccelerationDirection;
    public float m_AccelerationForce = 0.0f;
    public float m_Speed = 0.0f;
    public Vector3 m_SpeedDirection;
    public string m_Label;
    public List<Particle> m_ParticlesToIgnore;
    public AudioSource audioData;

    private void Awake()
    {
        ParticleManager.AddToParticleList(this);
        m_ParticlesToIgnore = new List<Particle>();
    }

    private void UpdateAcceleration()
    {
        Vector3 totalDirection = new Vector3();

        Vector3 myPosition = transform.position;
        Charge myCharge = m_Charge;
        Vector3 theirPosition;
        Vector3 chargeDirection;
        float sqrDistance = 1.0f;


        foreach (Particle particle in ParticleManager.GetChargedParticleList())
        {
            if (particle == this || m_ParticlesToIgnore.Contains(particle))
                continue; //Don't calculate yourself

            theirPosition = particle.transform.position;
            chargeDirection = theirPosition - myPosition;

            if (m_Charge == particle.m_Charge) //Repell
            {
                chargeDirection *= -1; //Invert vector
            } //Else vectors are properly oriented;

            sqrDistance = chargeDirection.sqrMagnitude;

            sqrDistance += 1; //Make sure it's not a decimal
            sqrDistance *= 1000.0f;

            Vector3 normalized = chargeDirection.normalized;

            float force = (1 / sqrDistance);

            normalized *= force;
            totalDirection += normalized; //Add the force to my total direction vector;

            m_AccelerationDirection = totalDirection;
            m_AccelerationForce = m_AccelerationDirection.sqrMagnitude;
            
        }
    }

    private void UpdateSpeed()
    {
        m_SpeedDirection += m_AccelerationDirection;
        m_Speed = m_SpeedDirection.sqrMagnitude;
    }
    private void UpdatePosition()
    {
        if (m_Charge == Charge.Neutral || m_Charge == Charge.Positive)
            return;
        transform.position += m_SpeedDirection;
    }
    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponent<OVRGrabbable>().isGrabbed)
        {
            UpdateAcceleration();
            UpdateSpeed();
            UpdatePosition();
        }
    }
}
