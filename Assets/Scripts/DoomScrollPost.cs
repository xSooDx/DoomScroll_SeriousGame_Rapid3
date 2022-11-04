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
    [SerializeField] TextMeshProUGUI likeText;
    [SerializeField] TextMeshProUGUI shareText;

    [SerializeField] RectTransform likeImgTransform;
    [SerializeField] RectTransform shareImgTransform;

    public float scaleFactor = 0.1f;
    public float pulseFrequency = 1f;
    public float pulseAmp = 1f;

    public ParticleSystem likeParticles;
    public ParticleSystem shareParticles;

    Coroutine likePulseRoutine = null;
    Coroutine sharePulseRoutine = null;

    Vector3 likeScale;
    Vector3 shareScale;

    [HideInInspector] public PhoneScreenController phoneScreenRef;
    public void Awake()
    {
        
    }

    public void Start()
    {
        imgComponent.sprite = postData.postImage;
        titleText.text = postData.postTitle;
        int likeCount = Random.Range(8, 99);
        likeText.text = likeCount + "k";
        shareText.text = Random.Range(1, likeCount) + "k";

        likeScale = likeImgTransform.localScale;
        shareScale = shareImgTransform.localScale;

        likePulseRoutine = StartCoroutine(LikeSizePulseCoroutine());
        sharePulseRoutine = StartCoroutine(ShareSizePulseCoroutine());
    }

    public void OnLike()
    {
        if (likePulseRoutine == null) return;
        phoneScreenRef.PostLike();
        likeParticles.Play();
        StopCoroutine(likePulseRoutine);
        likePulseRoutine = null;
        likeImgTransform.localScale = likeScale;
    }

    public void OnShare()
    {
        if (sharePulseRoutine == null) return;
        phoneScreenRef.PostShare();
        shareParticles.Play();
        StopCoroutine(sharePulseRoutine);
        sharePulseRoutine = null;
        shareImgTransform.localScale = shareScale;
    }

    IEnumerator LikeSizePulseCoroutine()
    {
        
        while (true)
        {
            float c = Mathf.Sin(Time.time * pulseFrequency) * pulseAmp;
            c *= c;
            c *= scaleFactor;
            likeImgTransform.localScale = likeScale + Vector3.one * c;
            yield return null;
        }
    }

    IEnumerator ShareSizePulseCoroutine()
    {
        
        while (true)
        {
            float c = Mathf.Sin(Time.time * pulseFrequency) * pulseAmp;
            c *= c;
            c *= scaleFactor;
            shareImgTransform.localScale = shareScale + Vector3.one * c;
            yield return null;
        }
    }
}
