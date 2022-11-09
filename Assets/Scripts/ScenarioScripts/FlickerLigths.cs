using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class FlickerLigths : MonoBehaviour
{
    public float minFlickerTime = 0.1f;
    public float maxFlickerTime = 0.3f;

    Coroutine flickering = null;

    float timeDelay;

    Light myLight;

    private void Start()
    {
        myLight = GetComponent<Light>();
    }


    public void StartFlicker()
    {
        StopFlicering(true);
        flickering = StartCoroutine(FlickeringLight());
    }

    public void StopFlicering(bool lightOn)
    {
        if(flickering != null)
        {
            StopCoroutine(flickering);
            flickering = null;
        }
        myLight.enabled = lightOn;
    }


    IEnumerator FlickeringLight()
    {
        while (true)
        {
            myLight.enabled = false;
            timeDelay = Random.Range(minFlickerTime, maxFlickerTime);
            yield return new WaitForSeconds(timeDelay);
            myLight.enabled = true;
            timeDelay = Random.Range(minFlickerTime, maxFlickerTime);
            yield return new WaitForSeconds(timeDelay);
        }
    }
}
