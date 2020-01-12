using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristButton : MonoBehaviour
{
    public AudioSource m_AudioSource;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "WristButton")
        {
            m_AudioSource.Play();
        }
    }
}
