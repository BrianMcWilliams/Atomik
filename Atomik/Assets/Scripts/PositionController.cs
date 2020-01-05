using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script to keep an object in the center of the camera 
public class PositionController : MonoBehaviour
{
    public Transform m_Camera;
    public float m_DistanceFromCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = m_Camera.position + m_Camera.forward * m_DistanceFromCamera;
        transform.position = pos;
    }
}
