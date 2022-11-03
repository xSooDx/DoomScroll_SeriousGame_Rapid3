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
    [SerializeField] PhoneScreenController phoneScreenController;

    // Start is called before the first frame update
    private void Awake()
    {
        xrInteractable = GetComponent<XRGrabInteractable>();
    }

    void Start()
    {
        xrInteractable.selectEntered.AddListener(OnPickUp);
        xrInteractable.selectExited.AddListener(OnDrop);
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
        Debug.Log("A");
        if (arg0.interactorObject is XRBaseControllerInteractor controllerInteractor)
        {
            Debug.Log("B");
            phoneScreenController.currentInteractor = controllerInteractor;
            //controllerInteractor.SendHapticImpulse(1, 1);
        }
        //throw new NotImplementedException();
    }

    private void OnDrop(SelectExitEventArgs arg0)
    {
        if (arg0.interactorObject is XRBaseControllerInteractor controllerInteractor && controllerInteractor == phoneScreenController.currentInteractor)
        {
            Debug.Log("B");
            phoneScreenController.currentInteractor = null;
            //controllerInteractor.SendHapticImpulse(1, 1);
        }
    }

    public void CorrectPhoneRotationForGrab()
    {

    }
}
