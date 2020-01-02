using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pointer : MonoBehaviour
{
    public float m_DefaultLength = 5.0f;
    public GameObject m_RayStartPosition;
    public EventSystem m_EvenySystem;
    public StandaloneInputModule m_InputModule;

    private LineRenderer m_LineRenderer;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        m_LineRenderer.SetPosition(0, m_RayStartPosition.transform.position);
        m_LineRenderer.SetPosition(1, GetEnd());
    }

    private float GetCanvasDistance()
    {
        //Get Data
        PointerEventData eventData = new PointerEventData(m_EvenySystem);
        eventData.position = m_InputModule.inputOverride.mousePosition;

        //Raycast using data
        List<RaycastResult> results = new List<RaycastResult>();
        m_EvenySystem.RaycastAll(eventData, results);

        //get closest
        RaycastResult closest = FindFirstRaycast(results);
        float distance = closest.distance;

        //clamp 
        distance = Mathf.Clamp(distance, 0.0f, m_DefaultLength);
        return distance;
    }

    private Vector3 GetEnd()
    {
        float distance = GetCanvasDistance();
        Vector3 endPosition = CalculateEnd(m_DefaultLength);

        if (distance != 0)
            endPosition = CalculateEnd(distance);

        return endPosition;
    }

    private RaycastResult FindFirstRaycast(List<RaycastResult> results)
    {
        foreach(RaycastResult result in results)
        {
            if (!result.gameObject)
                continue;

            return result;
        }
        return new RaycastResult();
    }

    private Vector3 CalculateEnd(float length)
    {
        return m_RayStartPosition.transform.position + (transform.forward * length);
    }

    private RaycastHit CreateRaycast(float length)
    {
        RaycastHit hit;
        Ray ray = new Ray(m_RayStartPosition.transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLength);
        return hit;
    }
}
