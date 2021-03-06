﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//God object to facilitate grabbing all the particles in the scene.
public class ParticleManager : MonoBehaviour
{
    public static List<Particle> m_ParticleList;
    public static List<Particle> m_ChargedParticleList;
    
    public static List<Tuple<Particle, Particle>> m_BindCandidates;
    public static void AddToParticleList(Particle particle)
    {
        if (m_ParticleList == null)
            m_ParticleList = new List<Particle>();
        m_ParticleList.Add(particle);

        if (m_ChargedParticleList == null)
        {
            m_ChargedParticleList = new List<Particle>();
        }
        if(particle.m_Charge != Charge.Neutral)
        {
            m_ChargedParticleList.Add(particle);
        }
    }
    public static List<Particle> GetParticleList()
    {
        if (m_ParticleList == null)
            m_ParticleList = new List<Particle>();

        return m_ParticleList;
    }
    public static List<Particle> GetChargedParticleList()
    {
        if (m_ChargedParticleList == null)
            m_ChargedParticleList = new List<Particle>();

        return m_ChargedParticleList;
    }

    public static void DestroyParticle(Particle particle)
    {
        if (particle)
        {
            //for protons and electrons, we need to also remove them from m_ChargedParticleList
            if (particle.m_Charge != Charge.Neutral)
                ParticleManager.GetChargedParticleList().Remove(particle);
            ParticleManager.m_ParticleList.Remove(particle);

            //get the particle system of the particle about to be destroyed and play it
          //  ParticleSystem explosion = Instantiate(particle.m_Explosion, particle.transform.position, particle.transform.rotation);
         //   explosion.Play();
         //   particle.GetComponent<Renderer>().enabled = false;

        //    Destroy(explosion, explosion.main.duration);
            Destroy(particle.gameObject);
        }
    }

    //Called when the "Clear Particles" button is clicked
    public void DestroyAllParticles()
    {
        foreach (Particle particle in m_ParticleList)
        {
            Destroy(particle.gameObject);
        }
        m_ParticleList.Clear();

        if (m_ChargedParticleList != null)
        {
            foreach (Particle particle in m_ChargedParticleList)
            {
                if(particle.m_Charge == Charge.Positive)
                {
                    particle.m_ParticlesToIgnore.Clear();
                    Destroy(particle.gameObject);
                }
            }
            m_ChargedParticleList.Clear();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetParticleList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
