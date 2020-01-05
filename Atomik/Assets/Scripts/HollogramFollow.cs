
using UnityEngine;
using System.Collections;

//Makes the hologram follow the player without rotating with the camera.
public class HollogramFollow : MonoBehaviour
{
    // The target we are following
    public Transform target;

    private void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    void LateUpdate()
    {
        Vector3 offset = new Vector3(0, 0, 0);
        transform.position = target.position + offset;
    }
}