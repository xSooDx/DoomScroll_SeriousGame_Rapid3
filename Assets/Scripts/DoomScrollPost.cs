using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DoomScrollPost : MonoBehaviour
{
    public DoomScrollPostData postData;
    [SerializeField] Image imgComponent;
    [SerializeField] TextMeshProUGUI titleText;
    public void Awake()
    {
        
    }

    public void Start()
    {
        imgComponent.sprite = postData.postImage;
        titleText.text = postData.postTitle;
    }
}
