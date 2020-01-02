using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//This script will handle input for UI events.

public class VRInput : BaseInput
{
    //This is attached to the right hand and will be the starting position of the pointer
    public Camera m_EventCamera = null;

    //The click button is the A button on the right controller
    public OVRInput.Button m_ClickButton = OVRInput.Button.One;
    public OVRInput.Controller m_Controller = OVRInput.Controller.RTouch;

    protected override void Awake()
    {
        GetComponent<BaseInputModule>().inputOverride = this;
    }

    public override bool GetMouseButtonDown(int button)
    {
        return OVRInput.GetDown(m_ClickButton, m_Controller);
    }

    public override bool GetMouseButton(int button)
    {
        return OVRInput.Get(m_ClickButton, m_Controller);
    }

    public override bool GetMouseButtonUp(int button)
    {
        return OVRInput.GetUp(m_ClickButton, m_Controller);
    }

    public override Vector2 mousePosition
    {
        //get the center point of the camera
        get
        {
            return new Vector2(m_EventCamera.pixelWidth / 2, m_EventCamera.pixelHeight / 2);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
