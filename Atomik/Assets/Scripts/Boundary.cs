using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public ParticleSystem m_Explosion;
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
            //for protons and electrons, we need to also remove them from m_ChargedParticleList
            if (particle.m_Charge != Charge.Neutral)
                ParticleManager.GetChargedParticleList().Remove(particle);
            ParticleManager.m_ParticleList.Remove(particle);

            ParticleSystem explosion = Instantiate(m_Explosion, particle.transform.position, Quaternion.identity);
            explosion.Play();
            Destroy(explosion, explosion.main.duration);

            Destroy(particle.gameObject);
        }
    }
}
