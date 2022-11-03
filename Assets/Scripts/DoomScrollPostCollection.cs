using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoomScrollCollection", menuName = "DoomScroll/DoomScrollPostCollection", order = 1)]
public class DoomScrollPostCollection : ScriptableObject
{
    public List<DoomScrollPostData> list = new List<DoomScrollPostData>();
}


[System.Serializable]
public class DoomScrollPostData
{
    public Sprite postImage;
    public string postTitle;
}

