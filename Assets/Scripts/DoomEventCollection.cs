using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "DoomEventCollection", menuName = "DoomScroll/DoomEventCollection", order = 1)]
public class DoomEventCollection : ScriptableObject
{
    public List<DoomEventTrigger> list = new List<DoomEventTrigger>();
}


[System.Serializable]
public class DoomEventTrigger
{
    public int triggerAtDoomScore;
    public UnityEvent doomEvent;
}
