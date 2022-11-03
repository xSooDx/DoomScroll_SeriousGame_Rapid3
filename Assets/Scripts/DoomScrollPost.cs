using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DoomScrollPost : MonoBehaviour
{
    public Sprite postImage;
    Image imgComponent;

    public void Awake()
    {
        imgComponent = GetComponent<Image>();
    }

    public void Start()
    {
        imgComponent.sprite = postImage;
    }
}
