using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HologramUI : MonoBehaviour
{
    public OVRCameraRig m_CameraRig;
    public Transform m_Hologram;
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
            //set the position of the hologram to just in front of the player and rotate it so its facing the player
            m_Hologram.position = m_CameraRig.centerEyeAnchor.position + m_CameraRig.centerEyeAnchor.forward * m_DistanceFromPlayer;
            m_Hologram.rotation = Quaternion.Euler(0.0f, m_CameraRig.centerEyeAnchor.rotation.eulerAngles.y, 0.0f);
        }
        else
        {
            m_Hidden = true;
        }
    }
}
