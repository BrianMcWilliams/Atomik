using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//God object to facilitate grabbing all the particles in the scene.
public class ParticleManager : MonoBehaviour
{
    public static List<Particle> m_ParticleList;
    public static void AddToParticleList(Particle particle)
    {
        if (m_ParticleList == null)
            m_ParticleList = new List<Particle>();
        m_ParticleList.Add(particle);
    }
    public static List<Particle> GetParticleList()
    {
        if (m_ParticleList == null)
            m_ParticleList = new List<Particle>();

        return m_ParticleList;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_ParticleList = new List<Particle>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
