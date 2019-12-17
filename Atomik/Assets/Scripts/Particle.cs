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
    public float m_AccelerationForce;
    public string m_Label;
    private void Awake()
    {
        ParticleManager.AddToParticleList(this);

        UpdateAcceleration();
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
            if (particle == this)
                continue; //Don't calculate yourself

            theirPosition = particle.transform.position;
            chargeDirection = theirPosition - myPosition;

            if (m_Charge == particle.m_Charge) //Repell
            {
                chargeDirection *= -1; //Invert vector
            } //Else vectors are properly oriented;

            sqrDistance = chargeDirection.sqrMagnitude;

            Vector3 normalized = chargeDirection.normalized;
            normalized *= (1 / sqrDistance); //Make attraction || repulsion smaller the further you are away;

            totalDirection += normalized; //Add the force to my total direction vector;
        }

        m_AccelerationDirection = totalDirection;
    }
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
        }
    }

    void OnGUI()
    {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        GUI.Box(new Rect(screenPos.x, Screen.height - screenPos.y, 100, 50), m_Label);
    } 
    private void UpdatePosition()
    {
        transform.position += m_AccelerationDirection;
    }
    // Update is called once per frame
    void Update()
    {
        UpdateAcceleration();
        UpdatePosition();
    }
}
