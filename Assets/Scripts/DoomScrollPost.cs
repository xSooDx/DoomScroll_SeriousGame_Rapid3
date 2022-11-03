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

    [SerializeField] Button btn;
    [HideInInspector] public PhoneScreenController phoneScreenRef;
    public void Awake()
    {
        
    }

    public void Start()
    {
        imgComponent.sprite = postData.postImage;
        titleText.text = postData.postTitle;
    }

    public void OnLike()
    {
        phoneScreenRef.PostLike();
    }

    public void OnShare()
    {
        phoneScreenRef.PostShare();
    }
}
