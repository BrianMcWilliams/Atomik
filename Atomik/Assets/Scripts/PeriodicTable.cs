using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// This line makes the asset show up in the Create Asset menus:
[CreateAssetMenu(fileName = "NewPeriodicTable.asset", menuName = "Custom Data/PeriodicTable")]
public class PeriodicTable : MonoBehaviour
{
    public PeriodicTableObj m_PeriodicTable;
    public string sourcePath;

    [System.Serializable]
    public class PeriodicTableObj
    {
        public List<Table> table;
        public List<Element> lanthanoids;
        public List<Element> actinoids;
    }

    [System.Serializable]
    public class Table
    {
        //public string wiki;
        public List<Element> elements;
    }

    [System.Serializable]
    public class Element
    {
        public string group;
        public int position;
        public string name;
        public int number;
        public string small;
        public float molar;
        public List<int> electrons;
    }

    public Element GetElement(List<Particle> candidateParticles, out string errorMessage)
    {
        errorMessage = null;

        //Count particles
        int protonCount = 0;
        int electronCount = 0;
        int neutronCount = 0;

        foreach (Particle particle in candidateParticles)
        {
            if (particle.m_Charge == Charge.Positive)
            {
                protonCount += 1;
            }
            else if (particle.m_Charge == Charge.Negative)
            {
                electronCount += 1;
            }
            else
            {
                neutronCount += 1;
            }
        }

        if (electronCount != protonCount)
        {
            errorMessage = "Stable elements have an equal number of \n Protons and Electron";
            return null;
        }

        return GetElementForElectronCount(electronCount, neutronCount, out errorMessage);
    }

    public Element GetElementForElectronCount(int electronCount, int neutronCount, out string errorMessage)
    {
        foreach (Table table in m_PeriodicTable.table)
        {
            foreach (Element element in table.elements)
            {
                if(electronCount == element.number)
                {
                    int elNeutronCount = Mathf.RoundToInt(element.molar) - element.number;
                    if(neutronCount == elNeutronCount)
                    {
                        errorMessage = null;
                        return element;
                    }
                    else if(neutronCount < elNeutronCount)
                    {
                        errorMessage = $"You don't have quite enough neutrons, \n {element.name} has {elNeutronCount}";
                        return null;
                    }
                    else //neutronCount > elNeutronCount
                    {
                        errorMessage = $"You seem to have too many neutrons, \n {element.name} has {elNeutronCount}";
                        return null;
                    }
                }
            }
        }

        foreach (Element element in m_PeriodicTable.lanthanoids)
        {
            if (electronCount == element.number)
            {
                int elNeutronCount = Mathf.RoundToInt(element.molar) - element.number;
                if (neutronCount == elNeutronCount)
                {
                    errorMessage = null;
                    return element;
                }
                else if (neutronCount < elNeutronCount)
                {
                    errorMessage = $"You don't have quite enough neutrons, \n {element.name} has {elNeutronCount}";
                    return null;
                }
                else //neutronCount > elNeutronCount
                {
                    errorMessage = $"You seem to have too many neutrons, \n {element.name} has {elNeutronCount}";
                    return null;
                }
            }
        }

        foreach (Element element in m_PeriodicTable.actinoids)
        {
            if (electronCount == element.number)
            {
                int elNeutronCount = Mathf.RoundToInt(element.molar) - element.number;
                if (neutronCount == elNeutronCount)
                {
                    errorMessage = null;
                    return element;
                }
                else if (neutronCount < elNeutronCount)
                {
                    errorMessage = $"You don't have quite enough neutrons, \n {element.name} has {elNeutronCount}";
                    return null;
                }
                else //neutronCount > elNeutronCount
                {
                    errorMessage = $"You seem to have too many neutrons, \n {element.name} has {elNeutronCount}";
                    return null;
                }
            }
        }

        errorMessage = "No known element has this many electrons";
        return null;
    }

    public void Import()
    {
        string jsonString = File.ReadAllText(sourcePath);
        m_PeriodicTable = JsonUtility.FromJson<PeriodicTableObj>(jsonString);
    }
    //public JSONObject m_PeriodicTableJSON =
}