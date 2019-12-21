using System;
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
