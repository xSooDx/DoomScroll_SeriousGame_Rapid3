using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;


public class GameManager : MonoBehaviour
{
    public List<DoomEventTrigger> doomEventCollection;
    int currentDoomIndex;

    [SerializeField] int doomPoints;

    public int DoomScore { get { return doomPoints; } }

    public static GameManager instance;

    public UnityEvent<int> onScoreUpdate;

    [SerializeField] Volume darknessVolume;
    [SerializeField] Volume grayscaleVolume;

    [SerializeField] float grayScaleMaxScore = 100f;
    [SerializeField] float darknessMaxScore = 200f;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        ResetGame();
    }

    private void Start()
    {
        grayscaleVolume.weight = 0;
        grayscaleVolume.priority = 0;
        darknessVolume.weight = 0;
        darknessVolume.priority = 1;
        onScoreUpdate.AddListener(OnDoomPointsUpdate);
    }

    void ResetGame()
    {
        currentDoomIndex = 0;
        doomPoints = 0;
    }

    public void AddDoomPoints(int points)
    {
        doomPoints = Mathf.Max(doomPoints + points, 0);
        onScoreUpdate.Invoke(doomPoints);
        if (currentDoomIndex >= doomEventCollection.Count) return;
        DoomEventTrigger eventTrigger = doomEventCollection[currentDoomIndex];
        if (doomPoints > eventTrigger.triggerAtDoomScore)
        {
            eventTrigger.doomEvent.Invoke();
            currentDoomIndex++;
        }
    }

    public void RemoveDoomPoints(int points)
    {
        doomPoints = Mathf.Max(doomPoints - points, 0);
        onScoreUpdate.Invoke(doomPoints);

        if (currentDoomIndex == 0) return;

        DoomEventTrigger eventTrigger = doomEventCollection[currentDoomIndex - 1];
        if (doomPoints < eventTrigger.triggerAtDoomScore)
        {
            eventTrigger.reverseDoomEvent.Invoke();
            currentDoomIndex--;
        }
    }

    void OnDoomPointsUpdate(int doomPoints)
    {
        LeanTween.value(grayscaleVolume.weight, Mathf.Min(doomPoints / grayScaleMaxScore, 1f), 0.1f)
            .setOnUpdate((float value) => grayscaleVolume.weight = value);


        LeanTween.value(darknessVolume.weight, Mathf.Min(Mathf.Max((doomPoints - grayScaleMaxScore), 0f) / (darknessMaxScore - grayScaleMaxScore), 1f), 0.1f)
            .setOnUpdate((float value) => darknessVolume.weight = value);
    }

    // Update is called once per frame
    void Update()
    {

    }
}



