﻿/************************************************************************************

Copyright   :   Copyright 2017-Present Oculus VR, LLC. All Rights reserved.

Licensed under the Oculus VR Rift SDK License Version 3.2 (the "License");
you may not use the Oculus VR Rift SDK except in compliance with the License,
which is provided at the time of installation or download, or which
otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at

http://www.oculusvr.com/licenses/LICENSE-3.2

Unless required by applicable law or agreed to in writing, the Oculus VR SDK
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

************************************************************************************/
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ControllerSelection {

    public class OVRPointerVisualizer : MonoBehaviour {
        [Header("(Optional) Tracking space")]
        [Tooltip("Tracking space of the OVRCameraRig.\nIf tracking space is not set, the scene will be searched.\nThis search is expensive.")]
        public Transform trackingSpace = null;
        [Header("Visual Elements")]
        [Tooltip("Line Renderer used to draw selection ray.")]
        public LineRenderer linePointer = null;
        [Tooltip("Fallback gaze pointer.")]
        public Transform gazePointer = null;
        [Tooltip("Visually, how far out should the ray be drawn.")]
        public float rayDrawDistance = 500;
        [Tooltip("How far away the gaze pointer should be from the camera.")]
        public float gazeDrawDistance = 3;

        [HideInInspector]
        public OVRInput.Controller activeController = OVRInput.Controller.RTouch;

        public Transform m_CanvasTransform;
        OVRRaycaster m_Raycaster;

        Ray m_SelectionRay;

        void Awake() {
            if (trackingSpace == null) {
                Debug.LogWarning("OVRPointerVisualizer did not have a tracking space set. Looking for one");
                trackingSpace = OVRInputHelpers.FindTrackingSpace();
            }

            m_Raycaster = m_CanvasTransform.GetComponent<OVRRaycaster>();
            m_SelectionRay.origin = m_Raycaster.m_PointerOrigin.position;
        }

        void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
            if (trackingSpace == null) {
                Debug.LogWarning("OVRPointerVisualizer did not have a tracking space set. Looking for one");
                trackingSpace = OVRInputHelpers.FindTrackingSpace();
            }
        }

        public void SetPointer(Ray ray) {
            if (linePointer != null) {
                linePointer.SetPosition(0, ray.origin);
                if (m_Raycaster.GetRayCastResultsCount() == 0)
                    linePointer.SetPosition(1, ray.origin + ray.direction * rayDrawDistance);
                else
                    linePointer.SetPosition(1, m_Raycaster.GetRaycastHitPosition());
            }

            if (gazePointer != null) {
                gazePointer.position = ray.origin + ray.direction * gazeDrawDistance;
            }
        }

        public void SetPointerVisibility() {
            if (trackingSpace != null && activeController != OVRInput.Controller.None && m_Raycaster.GetRayCastResultsCount() != 0) {
                if (linePointer != null)
                {
                    linePointer.enabled = true;
                }
                if (gazePointer != null)
                {
                    gazePointer.gameObject.SetActive(false);
                }
            }
            else {
                if (linePointer != null) {
                    linePointer.enabled = false;
                }
                if (gazePointer != null) {
                    gazePointer.gameObject.SetActive(true);
                }
            }
           
        }

        void Update() {
            activeController = OVRInputHelpers.GetControllerForButton(OVRInput.Button.PrimaryIndexTrigger, activeController);
            m_SelectionRay = OVRInputHelpers.GetSelectionRay(OVRInput.Controller.RTouch, trackingSpace);
            SetPointerVisibility();
            SetPointer(m_SelectionRay);
        }
    }
}