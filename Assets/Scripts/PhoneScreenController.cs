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
    [SerializeField] RectTransform scoreEffectSpawn;
    [SerializeField] TextMeshProUGUI scoreTextPrefab;
    [SerializeField] GameObject tutorial;

    [HideInInspector] public XRBaseControllerInteractor currentInteractor;

    [Range(0, 1f)] public float postHapticIntensity = 0.25f;
    [Range(0, 1f)] public float likeHapticIntensity = 0.5f;
    [Range(0, 1f)] public float shareHapticIntensity = 0.75f;

    [Range(0, 1f)] public float postHapticDuration = 0.2f;
    [Range(0, 1f)] public float likeHapticDuration = 0.4f;
    [Range(0, 1f)] public float shareHapticDuration = 0.6f;
    

    public float forwardDistance = 0.1f;

    [SerializeField] AudioClip shareSound;
    [SerializeField] AudioClip likeSound;
    [SerializeField] AudioClip scrollSound;
    [SerializeField] AudioSource postAudioSource;
    [SerializeField] AudioSource actionAudioSource;

    Coroutine audioCoroutine = null;

    float postHeight;
    int currentPostIndex = 0;
    bool inputEnabled = false;

    RectTransform currPost = null;
    RectTransform prevPost = null;

    bool isScrolling = false;

    public float scrollTime = 0.3f;

    public float audioFadeInOutTime = 0.5f;

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
        Vector3 tmp = currPost.localPosition;
        tmp.y = -postHeight * 1.5f;
        currPost.localPosition = tmp;
        prevPost.LeanMoveLocalY(postHeight / 2, scrollTime)
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
        currentPostIndex = (currentPostIndex + 1) % postCollection.list.Count;
        //if (currentPostIndex < postCollection.list.Count - 1)
        //{
        //currentPostIndex++;
        actionAudioSource.PlayOneShot(scrollSound, 0.4f);
        LoadPost(currentPostIndex);
        PostView();
        tutorial.SetActive(false);
        //}

    }

    public void PostView()
    {
        DoomScrollPostData data = postCollection.list[currentPostIndex];
        GameManager.instance.AddDoomPoints(data.postScore);
        SpawnScoreTextEffect(data.postScore);
        if (data.postAudioClip != null)
        {
            audioCoroutine = StartCoroutine(PlaySoundEffect(data.postAudioClip));
        }
        currentInteractor.SendHapticImpulse(postHapticIntensity, postHapticDuration);
    }

    public void PostLike()
    {
        actionAudioSource.PlayOneShot(likeSound);
        int score = postCollection.list[currentPostIndex].postScore * 2;
        GameManager.instance.AddDoomPoints(score);
        SpawnScoreTextEffect(score);
        currentInteractor.SendHapticImpulse(likeHapticIntensity, likeHapticDuration);
        tutorial.SetActive(false);
    }

    public void PostShare()
    {
        actionAudioSource.PlayOneShot(shareSound);
        int score = postCollection.list[currentPostIndex].postScore * 4;
        GameManager.instance.AddDoomPoints(score);
        SpawnScoreTextEffect(score);
        currentInteractor.SendHapticImpulse(shareHapticIntensity, shareHapticDuration);
        tutorial.SetActive(false);
    }

    public void ActivateInput()
    {
        //XRUIInputModule
        //inputEnabled = true;
    }

    void SpawnScoreTextEffect(int score)
    {
        TextMeshProUGUI text = Instantiate(scoreTextPrefab, scoreEffectSpawn);
        text.text = "+" + score;
        LeanTween.scale(text.gameObject, text.transform.localScale * 1.25f, 1f).setEaseOutElastic();
        text.transform.LeanMoveLocalY(text.transform.position.y + postHeight / 2, 1f);
        text.transform.LeanMoveLocalZ(text.transform.position.z - postHeight / 2, 1f)
            .setOnComplete(() => Destroy(text.gameObject));

        
    }

    public void DeactivateInput()
    {
        inputEnabled = false;
    }

    public IEnumerator PlaySoundEffect(AudioClip clip, float targetVolume = 1)
    {
        if (audioCoroutine != null)
        {
            StopCoroutine(audioCoroutine);
            audioCoroutine = null;
        }

        postAudioSource.Stop();
        postAudioSource.clip = clip;
        postAudioSource.volume = 0;
        postAudioSource.Play();
        float time = 0;
        while (time < audioFadeInOutTime)
        {
            postAudioSource.volume = time / audioFadeInOutTime;
            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(clip.length - audioFadeInOutTime * 2);
        while (time > 0)
        {
            postAudioSource.volume = time / audioFadeInOutTime;
            time -= Time.deltaTime;
            yield return null;
        }
    }

}
