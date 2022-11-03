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
        if (content.childCount > 0) Destroy(content.GetChild(0).gameObject);

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
        newPost.phoneScreenRef = this;
    }

    public void GoToNextPost()
    {
        if (currentPostIndex < postCollection.list.Count)
        {
            currentPostIndex++;
            LoadPost(currentPostIndex);
        }
        GameManager.instance.AddDoomPoints(10);
        PostView();
    }

    public void PostView()
    {
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
