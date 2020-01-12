using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracker : MonoBehaviour
{
    [SerializeField]
    OVRCameraRig cameraRig;

    [SerializeField]
    Transform leftHandTransform;

    [SerializeField]
    Transform rightHandTransform;

    void Start()
    {
        cameraRig.UpdatedAnchors += OnUpdatedAnchors;
    }

    private void OnDestroy()
    {
        cameraRig.UpdatedAnchors -= OnUpdatedAnchors;
    }

    private void OnUpdatedAnchors(OVRCameraRig rig)
    {
        leftHandTransform.SetPositionAndRotation(rig.leftHandAnchor.position, rig.leftHandAnchor.rotation);
        rightHandTransform.SetPositionAndRotation(rig.rightHandAnchor.position, rig.rightHandAnchor.rotation);
    }
}