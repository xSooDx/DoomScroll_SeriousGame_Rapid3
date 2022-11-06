using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    public DoomEventCollection doomEventCollection;
    int currentDoomIndex;

    [SerializeField] int doomPoints;

    public int DoomScore { get { return doomPoints; } }

    public static GameManager instance;

    public UnityEvent<int> onScoreUpdate;


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

    void ResetGame()
    {
        currentDoomIndex = 0;
        doomPoints = 0;
    }

    public void AddDoomPoints(int points)
    {
        doomPoints = Mathf.Max(doomPoints + points, 0);
        onScoreUpdate.Invoke(doomPoints);
        if (currentDoomIndex >= doomEventCollection.list.Count) return;
        DoomEventTrigger eventTrigger = doomEventCollection.list[currentDoomIndex];
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

        DoomEventTrigger eventTrigger = doomEventCollection.list[currentDoomIndex-1];
        if (doomPoints < eventTrigger.triggerAtDoomScore)
        {
            eventTrigger.reverseDoomEvent.Invoke();
            currentDoomIndex--;
        }
    }

    void OnDoomPointsUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}



