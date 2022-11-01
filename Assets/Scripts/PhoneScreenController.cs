using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneScreenController : MonoBehaviour
{
    [SerializeField] DoomScrollPostCollection postCollection;
    [SerializeField] DoomScrollPost postPrefab;
    [SerializeField] ScrollRect scrollRect;


    void Start()
    {
        scrollRect.onValueChanged.AddListener(OnScrollEvent);
        foreach (DoomScrollPostData post in postCollection.list)
        {
            CreatePost(post);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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

}
