using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//God object to facilitate grabbing all the particles in the scene.
public class ParticleManager : MonoBehaviour
{
    public static List<Particle> m_ParticleList;

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
