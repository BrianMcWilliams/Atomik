using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabParticle : MonoBehaviour
{
    public GameObject m_particle;

    public GameObject m_leftHand;
    public GameObject m_rightHand;

    //the distance a hand needs to be to grab a particle from the box
    public float m_grabDistance;

    OVRGrabber m_leftGrabber;
    OVRGrabber m_rightGrabber;

    // Start is called before the first frame update
    void Start()
    {
        m_leftGrabber = m_leftHand.GetComponent<OVRGrabber>();
        m_rightGrabber = m_rightHand.GetComponent<OVRGrabber>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToLeftHand = Vector3.Distance(this.transform.position, m_leftHand.transform.position);
        float distanceToRightHand = Vector3.Distance(this.transform.position, m_rightHand.transform.position);

        //checks for when the left trigger is pressed down and the left hand is near enough to the box
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch) && distanceToLeftHand < m_grabDistance)
        {
            //instantiate a particle and add it as a grab candidate for the hand
            GameObject particle = Instantiate(m_particle, m_leftHand.transform.position, Quaternion.identity);
            OVRGrabbable grabbable = particle.GetComponent<OVRGrabbable>();
            m_leftGrabber.AddGrabCandidate(grabbable);
        }

        //checks for when the right trigger is pressed down and the right hand is near enough to the box
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch) && distanceToRightHand < m_grabDistance)
        {
            //instantiate a particle and add it as a grab candidate for the hand
            GameObject particle = Instantiate(m_particle, m_rightHand.transform.position, Quaternion.identity);
            OVRGrabbable grabbable = particle.GetComponent<OVRGrabbable>();
            m_rightGrabber.AddGrabCandidate(grabbable);
        }
    }
}
