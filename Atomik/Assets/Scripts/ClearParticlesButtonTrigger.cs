using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearParticlesButtonTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Button")
        {
            ParticleManager particleManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ParticleManager>();
            particleManager.RemoveParticles();
        }
    }
}
