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
    private void Awake()
    {
        ParticleManager.AddToParticleList(this);

        UpdateAcceleration();
    }

    private void UpdateAcceleration()
    {
        Vector3 totalDirection = new Vector3();

        Vector3 myPosition = transform.position;
        Vector3 theirPosition;
        Vector3 chargeDirection;
        float distance = 1.0f;

        foreach (Particle particle in ParticleManager.m_ParticleList)
        {

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
