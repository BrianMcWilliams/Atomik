using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static PeriodicTable;

public class AtomChamber : MonoBehaviour
{

    public List< Tuple< Particle, float >> m_ParticlesInside = new List< Tuple< Particle, float >>();
    public List<Particle> m_CandidateParticles = new List<Particle>();
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

        Tuple<Particle, float> tupToRemove = null;
        foreach(Tuple<Particle, float> tup in m_ParticlesInside)
        {
            if (tup.Item1 == particle)
            {
                tupToRemove = tup;

                GameObject particleGO = tup.Item1.gameObject;
                GameObject timerGO = particleGO.transform.Find("Timer").gameObject;

                TextMesh timerText = timerGO.GetComponent<TextMesh>();
                timerText.text = null;

                break;
            }
        }

        if(tupToRemove != null)
            m_ParticlesInside.Remove(tupToRemove);

        if (m_CandidateParticles.Contains(particle))
            m_CandidateParticles.Remove(particle);

    }


    // Update is called once per frame
    void Update()
    {
        List<Tuple<Particle, float>> tupsToRemove = new List<Tuple<Particle, float>>();

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

                m_CandidateParticles.Add(tup.Item1);

                //Play feedback sound
                AudioSource audioData = GetComponent<AudioSource>();
                audioData.Play(0);

                tupsToRemove.Add(tup);

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

        foreach (Tuple<Particle,float> tup in tupsToRemove)
        {
            m_ParticlesInside.Remove(tup);
        }

        EvaluateElement();
    }

    public void ResetCanvas()
    {
        GameObject atomSymbol = transform.parent.Find("CurrentElementCanvas/AtomSymbol").gameObject;
        TextMeshProUGUI symbolText = atomSymbol.GetComponent<TextMeshProUGUI>();

        symbolText.text = null;

        GameObject elDetails = transform.parent.Find("CurrentElementCanvas/ElementDetails").gameObject;
        TextMeshProUGUI elDetailsText = elDetails.GetComponent<TextMeshProUGUI>();

        elDetailsText.text = null;

        GameObject errorHint = transform.parent.Find("CurrentElementCanvas/ErrorHint").gameObject;
        TextMeshProUGUI errorText = errorHint.GetComponent<TextMeshProUGUI>();

        errorText.text = "Waiting for particles";
    }
    public void EvaluateElement()
    {
        if (m_CandidateParticles.Count == 0)
        {
            ResetCanvas();
            return;
        }


        GameObject gameManagerObj = GameObject.FindGameObjectWithTag("GameManager");
        PeriodicTable periodicTable = gameManagerObj.GetComponent<PeriodicTable>();
        string errorMessage = null;
        Element el = periodicTable.GetElement(m_CandidateParticles, out errorMessage);
        
        if (el != null) //Handle success
        {
            SetCanvasInfo(el.small);
            SetCanvasDetails(el);
        }
        else
        {
            SetCanvasError(errorMessage);
        }
    }

    public void SetCanvasInfo(string symbol)
    {
        
        GameObject errorHint = transform.parent.Find("CurrentElementCanvas/ErrorHint").gameObject;
        TextMeshProUGUI errorText = errorHint.GetComponent<TextMeshProUGUI>();

        errorText.text = null;

        GameObject atomSymbol = transform.parent.Find("CurrentElementCanvas/AtomSymbol").gameObject;
        TextMeshProUGUI symbolText = atomSymbol.GetComponent<TextMeshProUGUI>();

        symbolText.text = symbol;
    }

    public void SetCanvasDetails(Element el)
    {
        GameObject errorHint = transform.parent.Find("CurrentElementCanvas/ErrorHint").gameObject;
        TextMeshProUGUI errorText = errorHint.GetComponent<TextMeshProUGUI>();

        errorText.text = null;

        GameObject elDetails = transform.parent.Find("CurrentElementCanvas/ElementDetails").gameObject;
        TextMeshProUGUI elDetailsText = elDetails.GetComponent<TextMeshProUGUI>();

        elDetailsText.text = $"You made {el.name}! \nIt consists of {el.electrons.Count} electron(s) and {el.electrons.Count} proton(s), \n{el.name} has a weight of {el.molar}";
    }
    public void SetCanvasError(string error)
    {
        GameObject atomSymbol = transform.parent.Find("CurrentElementCanvas/AtomSymbol").gameObject;
        TextMeshProUGUI symbolText = atomSymbol.GetComponent<TextMeshProUGUI>();

        symbolText.text = null;

        GameObject elDetails = transform.parent.Find("CurrentElementCanvas/ElementDetails").gameObject;
        TextMeshProUGUI elDetailsText = elDetails.GetComponent<TextMeshProUGUI>();

        elDetailsText.text = null;

        GameObject errorHint = transform.parent.Find("CurrentElementCanvas/ErrorHint").gameObject;
        TextMeshProUGUI errorText = errorHint.GetComponent<TextMeshProUGUI>();

        errorText.text = error;
    }
}
