using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.UI;

public class PhoneScreenController : MonoBehaviour
{
    [SerializeField] DoomScrollPostCollection postCollection;
    [SerializeField] TrackedDeviceGraphicRaycaster tdgr;
    [SerializeField] DoomScrollPost postPrefab;
    [SerializeField] ScrollRect scrollRect;

    float postHeight;

    bool inputEnabled = false;

    void Start()
    {
        scrollRect.onValueChanged.AddListener(OnScrollEvent);
        foreach (DoomScrollPostData post in postCollection.list)
        {
            CreatePost(post);
        }
        postHeight = postPrefab.GetComponent<RectTransform>().rect.height;
    }

    private void Update()
    {
        if (inputEnabled)
        {

        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

        float div = scrollRect.content.transform.localPosition.y / postHeight;
        float fraction = div - (long)div;
        if (fraction > 0.01f && fraction < 0.99f)
        {
            if (fraction < 0.5)
            {
                scrollRect.content.transform.position -= scrollRect.content.transform.up * Time.deltaTime * 0.05f;
            }
            else if (fraction >= 0.5)
            {
                scrollRect.content.transform.position += scrollRect.content.transform.up * Time.deltaTime * 0.1f;
            }
        }

        Debug.Log("LateUpdate " + postHeight + ", " + scrollRect.content.transform.localPosition.y);
        //scrollRect.content.transform.position += scrollRect.content.transform.up * Time.deltaTime * 0.05f;
    }

    void CreatePost(DoomScrollPostData data)
    {
        DoomScrollPost newPost = Instantiate(postPrefab, scrollRect.content);
        newPost.postImage = data.postImage;
        // Set newPostData
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
