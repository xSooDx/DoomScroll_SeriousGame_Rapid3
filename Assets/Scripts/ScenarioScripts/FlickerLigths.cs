using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLigths : MonoBehaviour
{
    public bool startFlickering = false;
    public bool isFlickering = false;
    public float timeDelay;

    // Update is called once per frame
    void Update()
    {
        if (startFlickering == true)
        {
            StartCoroutine(FlickeringLight());

        }
        else
        {
            StopCoroutine(FlickeringLight());
        }
    }
    IEnumerator FlickeringLight()
    {
        isFlickering = true;
        this.gameObject.GetComponent<Light>().enabled = false;
        timeDelay = Random.Range(.1f, 1f);
        yield return new WaitForSeconds(timeDelay);
        this.gameObject.GetComponent<Light>().enabled = true;
        timeDelay = Random.Range(.1f, 1f);
        yield return new WaitForSeconds(timeDelay);
        isFlickering = false;
    }
}