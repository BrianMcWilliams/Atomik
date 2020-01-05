﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HologramUI : MonoBehaviour
{
    public Transform m_Camera;
    public Transform m_Hologram;
    //The spawn point of the hologram which should always be where the camera is currently facing.
    public Transform m_HologramPosition;
    //The button that is on the left wrist. 
    public Button m_HologramButton;
    public float m_DistanceFromPlayer;

    bool m_Hidden;

    // Start is called before the first frame update
    void Start()
    {
        m_Hidden = true;
        m_HologramButton.onClick.AddListener(SetHologram);   
    }

    // Update is called once per frame
    void Update()
    {
        if (m_Hidden)
            m_Hologram.gameObject.SetActive(false);
        else
            m_Hologram.gameObject.SetActive(true); 
 
    }

    //Sets the hologram to hidden or visible
    void SetHologram()
    {
        if (m_Hidden)
        {
            m_Hidden = false;
            Vector3 pos = m_HologramPosition.position;
            m_Hologram.position = pos;
            m_Hologram.rotation = m_HologramPosition.rotation;
        }
        else
        {
            m_Hidden = true;
        }
    }
}
