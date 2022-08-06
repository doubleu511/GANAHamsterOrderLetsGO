using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Subtitle Scriptable Object", menuName = "ScriptableObjects/Subtitle Scriptable Object")]
public class SubtitleSO : ScriptableObject
{
    public bool isSkipable;
    public Subtitle[] subtitles;
}

[System.Serializable]
public class Subtitle
{
    [TextArea(2, 4)]
    public string[] texts;
}

