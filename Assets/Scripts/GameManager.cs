using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public DoomEventCollection doomEventCollection;
    int currentDoomIndex;

    int doomPoints;

    public static GameManager instance;



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
        doomPoints += points;
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



