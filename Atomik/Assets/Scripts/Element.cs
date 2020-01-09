//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UnityEngine.HoloToolkit.MRDL.PeriodicTable
{
    public class Element : MonoBehaviour
    {
        public static Element ActiveElement;

        public TextMesh ElementNumber;
        public TextMesh ElementName;
        public TextMesh ElementNameDetail;

        public TextMeshProUGUI ElementDescription;
        public Text DataAtomicNumber;
        public Text DataAtomicWeight;
        public Text DataMeltingPoint;
        public Text DataBoilingPoint;

        public Renderer BoxRenderer;
        public MeshRenderer[] PanelSides;
        public MeshRenderer PanelFront;
        public MeshRenderer PanelBack;
        public MeshRenderer[] InfoPanels;

        public Atom Atom;

        [HideInInspector]
        public ElementData data;

        private BoxCollider boxCollider;
        private PresentToPlayer present;

        public void SetActiveElement()
        {
            Element element = gameObject.GetComponent<Element>();
            ActiveElement = element;
        }

        public void ResetActiveElement()
        {
            ActiveElement = null;
        }

        public void Start()
        {
            // Turn off our animator until it's needed
            //GetComponent<Animator>().enabled = false;
            BoxRenderer.enabled = true;
            present = GetComponent<PresentToPlayer>();
        }

        public void Open()
        {
            if (present.Presenting)
                return;

            StartCoroutine(UpdateActive());
        }

        public IEnumerator UpdateActive()
        {
            present.Present();

            while (!present.InPosition)
            {
                // Wait for the item to be in presentation distance before animating
                yield return null;
            }

            // Start the animation
            Animator animator = gameObject.GetComponent<Animator>();
            animator.enabled = true;
            animator.SetBool("Opened", true);

            //Color elementNameColor = ElementName.GetComponent<MeshRenderer>().material.color;

            while (Element.ActiveElement == this)
            {
                //ElementName.GetComponent<MeshRenderer>().material.color = elementNameColor;
                // Wait for the player to send it back
                yield return null;
            }

            animator.SetBool("Opened", false);

            yield return new WaitForSeconds(0.66f); // TODO get rid of magic number        

            // Return the item to its original position
            present.Return();
        }


        /**
         * Set the display data for this element based on the given parsed JSON data
         */
        public void SetFromElementData(ElementData data)
        {
            this.data = data;

            ElementNumber.text = data.number;
            ElementName.text = data.symbol;
            ElementNameDetail.text = data.name;

            ElementDescription.text = data.summary;
            DataAtomicNumber.text = data.number;
            DataAtomicWeight.text = data.atomic_mass.ToString();
            DataMeltingPoint.text = data.melt.ToString();
            DataBoilingPoint.text = data.boil.ToString();

            Atom.NumElectrons = int.Parse(data.number);
            Atom.NumNeutrons = (int)data.atomic_mass / 2;
            Atom.NumProtons = (int)data.atomic_mass / 2;
            Atom.Radius = data.atomic_mass / 157 * 0.02f;//TEMP

            BoxRenderer.enabled = false;

            // Set our name so the container can alphabetize
            transform.parent.name = data.name;
        }
    }
}
