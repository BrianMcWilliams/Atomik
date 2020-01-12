using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        //destroy the particle when it exits the boundary
        Particle particle = other.gameObject.GetComponent<Particle>();
        if(particle)
        {
            //since neutrons are not in the list, no need to call Remove()
            if(particle.m_Charge != Charge.Neutral)
                ParticleManager.GetChargedParticleList().Remove(particle);
            ParticleManager.m_ParticleList.Remove(particle);
            Destroy(particle.gameObject);
        }
    }
}
