using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRGrabInteractable), typeof(Rigidbody))]
public class MobileXRController : MonoBehaviour
{
    XRGrabInteractable xrInteractable;
    [SerializeField] Transform leftHandGrabPivot;
    [SerializeField] Transform rightHandGrabPivot;
    [SerializeField] PhoneScreenController phoneScreenController;
    [SerializeField] GameObject phoneScreen;
    [SerializeField] GameObject blankScreen;
    [SerializeField] Rigidbody rb;

    // Start is called before the first frame update
    private void Awake()
    {
        xrInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
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
        rb.isKinematic = false;
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
        phoneScreen.SetActive(true);
        blankScreen.SetActive(false);
    }

    private void OnDrop(SelectExitEventArgs arg0)
    {
        if (arg0.interactorObject is XRBaseControllerInteractor controllerInteractor && controllerInteractor == phoneScreenController.currentInteractor)
        {
            Debug.Log("B");
            phoneScreenController.currentInteractor = null;
            //controllerInteractor.SendHapticImpulse(1, 1);
            phoneScreen.SetActive(false);
            blankScreen.SetActive(true);
        }
        rb.isKinematic = false;
    }

    public void CorrectPhoneRotationForGrab()
    {

    }
}
