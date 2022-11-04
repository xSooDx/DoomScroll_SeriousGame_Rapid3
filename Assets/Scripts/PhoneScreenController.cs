using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.UI;
using UnityEngine.XR.OpenXR.Input;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class PhoneScreenController : MonoBehaviour
{
    [SerializeField] DoomScrollPostCollection postCollection;
    [SerializeField] TrackedDeviceGraphicRaycaster tdgr;
    [SerializeField] DoomScrollPost postPrefab;
    [SerializeField] RectTransform content;
    [SerializeField] TextMeshProUGUI scoreText;


    [HideInInspector] public XRBaseControllerInteractor currentInteractor;

    [Range(0, 1f)] public float postHapticIntensity = 0.25f;
    [Range(0, 1f)] public float likeHapticIntensity = 0.5f;
    [Range(0, 1f)] public float shareHapticIntensity = 0.75f;

    [Range(0, 1f)] public float postHapticDuration = 0.2f;
    [Range(0, 1f)] public float likeHapticDuration = 0.4f;
    [Range(0, 1f)] public float shareHapticDuration = 0.6f;

    float postHeight;
    int currentPostIndex = 0;
    bool inputEnabled = false;

    RectTransform currPost = null;
    RectTransform prevPost = null;

    bool isScrolling = false;

    public float scrollTime = 0.3f;

    void Start()
    {
        scoreText.text = GameManager.instance.DoomScore.ToString();
        GameManager.instance.onScoreUpdate.AddListener((int score) =>
        {
            scoreText.text = score.ToString();
        });
        LoadPost(currentPostIndex);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //scrollRect.content.transform.position += scrollRect.content.transform.up * Time.deltaTime * 0.05f;
    }

    void LoadPost(int idx)
    {
        if (idx < postCollection.list.Count)
        {
            DoomScrollPostData data = postCollection.list[idx];
            prevPost = currPost;
            currPost = CreatePost(data).GetComponent<RectTransform>();
            postHeight = currPost.rect.height;
            PostScroll();

        }
    }
    void PostScroll()
    {
        if (prevPost == null) return;
        isScrolling = true;
        Debug.Log("postHeight " + postHeight);
        Vector3 tmp = currPost.localPosition;
        tmp.y = -postHeight * 1.5f;
        currPost.localPosition = tmp;
        prevPost.LeanMoveLocalY(postHeight/2, scrollTime)
            .setOnComplete(() =>
                Destroy(prevPost.gameObject)
            ); ;
        currPost.LeanMoveLocalY(-postHeight / 2, scrollTime).setOnComplete(() => isScrolling = false);
    }

    DoomScrollPost CreatePost(DoomScrollPostData data)
    {
        DoomScrollPost newPost = Instantiate(postPrefab, content);
        newPost.postData = data;
        newPost.phoneScreenRef = this;
        return newPost;
    }

    public void GoToNextPost()
    {
        if (isScrolling) return;
        if (currentPostIndex < postCollection.list.Count)
        {
            currentPostIndex++;
            LoadPost(currentPostIndex);
            PostView();
        }
        
    }

    public void PostView()
    {
        GameManager.instance.AddDoomPoints(10);
        currentInteractor.SendHapticImpulse(postHapticIntensity, postHapticDuration);
    }

    public void PostLike()
    {
        GameManager.instance.AddDoomPoints(25);
        currentInteractor.SendHapticImpulse(likeHapticIntensity, likeHapticDuration);
    }

    public void PostShare()
    {
        GameManager.instance.AddDoomPoints(50);
        currentInteractor.SendHapticImpulse(shareHapticIntensity, shareHapticDuration);
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
