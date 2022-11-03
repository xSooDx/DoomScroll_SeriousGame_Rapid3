using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoomScrollPost : MonoBehaviour
{
    public Sprite postImage;
    [SerializeField] Image imgComponent;

    public void Awake()
    {
        
    }

    public void Start()
    {
        imgComponent.sprite = postImage;
    }
}
