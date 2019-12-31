using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtomChamber : MonoBehaviour
{

    public List< Tuple< Particle, float >> m_ParticlesInside = new List< Tuple< Particle, float >>();
    // Start is called before the first frame update

    private void OnTriggerEnter(Collider other)
    {
        Particle particle = other.GetComponent<Particle>();

        if(particle != null)
            m_ParticlesInside.Add(Tuple.Create(particle, Time.time));
    }

    private void OnTriggerExit(Collider other)
    {
        Particle particle = other.GetComponent<Particle>();

        foreach(Tuple<Particle, float> tup in m_ParticlesInside)
        {
            if (tup.Item1 == particle)
            {
                m_ParticlesInside.Remove(tup);

                GameObject particleGO = tup.Item1.gameObject;
                GameObject timerGO = particleGO.transform.Find("Timer").gameObject;

                TextMesh timerText = timerGO.GetComponent<TextMesh>();
                timerText.text = null;

                return;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        foreach (Tuple<Particle, float> tup in m_ParticlesInside)
        {
            float timeElapsed = (Time.time - tup.Item2);
            if ( timeElapsed >= 10.0f)
            {
                GameObject particleGO = tup.Item1.gameObject;
                GameObject timerGO = particleGO.transform.Find("Timer").gameObject;

                TextMesh timerText = timerGO.GetComponent<TextMesh>();

                timerText.text = "♪♪♪♪♪";
                timerText.color = Color.blue;
                //Create Atom
            }
            else
            {
                GameObject particleGO = tup.Item1.gameObject;
                GameObject timerGO = particleGO.transform.Find("Timer").gameObject;

                TextMesh timerText = timerGO.GetComponent<TextMesh>();
                timerText.text = timeElapsed.ToString("F2");

                timerText.color = Color.Lerp(Color.gray, Color.green, (timeElapsed / 10.0f));
            }

        }
    }

}
