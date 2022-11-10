using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(AudioSource))]
public class PicFrame : MonoBehaviour
{

    Rigidbody rb;
    public float pushForce = 2f;
    public float minImpactVelForSound = 1f;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PushFrame()
    {
        rb.isKinematic = false;
        rb.AddForceAtPosition((3 * transform.forward  + Random.onUnitSphere).normalized * pushForce, transform.position + transform.up, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("pIC:  " + collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > minImpactVelForSound)
        {
            audioSource.Play();
        }
    }

}
