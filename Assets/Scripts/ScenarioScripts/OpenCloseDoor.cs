using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OpenCloseDoor : MonoBehaviour
{
    public GameObject doortoRotate;
    public float openSpeed;
    private bool isOpen = false;
    public AudioSource openSound;
    public AudioSource closeSound;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.O))
            {
                if (isOpen == false)
                {
                    transform.Rotate(0, 90, 0);
                    isOpen = true;
                    //openSound.Play();
                } else
                {
                    transform.Rotate(0, -90, 0);
                    isOpen = false;
                    closeSound.Play();
                }
                
            }

    }
}
