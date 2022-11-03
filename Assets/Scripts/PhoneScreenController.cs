using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.XR.OpenXR.Input;

public class PhoneScreenController : MonoBehaviour
{
    [SerializeField] DoomScrollPostCollection postCollection;
    [SerializeField] TrackedDeviceGraphicRaycaster tdgr;
    [SerializeField] DoomScrollPost postPrefab;
    [SerializeField] RectTransform content;

    [SerializeField] InputActionReference leftHapticAction;
    [SerializeField] InputActionReference rightHapticAction;

    float postHeight;
    int currentPostIndex = 0;
    bool inputEnabled = false;

    void Start()
    {
        LoadPost(currentPostIndex);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //scrollRect.content.transform.position += scrollRect.content.transform.up * Time.deltaTime * 0.05f;
    }

    void LoadPost(int idx)
    {
        if (content.childCount > 0) Destroy(content.GetChild(0).gameObject) ;

        if (idx < postCollection.list.Count)
        {
            DoomScrollPostData data = postCollection.list[idx];
            CreatePost(data);
        }
    }

    void CreatePost(DoomScrollPostData data)
    {
        DoomScrollPost newPost = Instantiate(postPrefab, content);
        newPost.postData = data;
    }

    public void GoToNextPost()
    {
        if (currentPostIndex < postCollection.list.Count)
        {
            currentPostIndex++;
            LoadPost(currentPostIndex);
        }
        GameManager.instance.AddDoomPoints(10);
        //OpenXRInput.SendHapticImpulse(leftHapticAction, 1, 1);
    }

    void OnScrollEvent(Vector2 pos)
    {

        Debug.Log("OnScrollEvent " + pos);
    }


    public void ActivateInput()
    {
        //XRUIInputModule
        //inputEnabled = true;
    }

    public void DeactivateInput()
    {
        inputEnabled = false;
    }

}
