using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable))]
public class MobileXRController : MonoBehaviour
{
    XRGrabInteractable xrInteractable;
    [SerializeField] Transform leftHandGrabPivot;
    [SerializeField] Transform rightHandGrabPivot;

    // Start is called before the first frame update
    private void Awake()
    {
        xrInteractable = GetComponent<XRGrabInteractable>();
    }

    void Start()
    {
        xrInteractable.selectEntered.AddListener(OnPickUp);
    }



    // Update is called once per frame
    void Update()
    {

    }

    private void OnPickUp(SelectEnterEventArgs arg0)
    {

        Debug.Log("OnPickup " + arg0.interactorObject.transform.tag);
        if (arg0.interactorObject.transform.CompareTag("LeftHand"))
        {
            xrInteractable.attachTransform = leftHandGrabPivot;
        }
        else if (arg0.interactorObject.transform.CompareTag("RightHand"))
        {
            xrInteractable.attachTransform = rightHandGrabPivot;
        }

        //throw new NotImplementedException();
    }

    public void CorrectPhoneRotationForGrab()
    {
        
    }
}
