using UnityEngine;

[System.Serializable]  // lets you embed a class (Sound) with sub properties in the inspector within another component (Audio Manager)
public class Setup //: ScriptableObject
{
    public string Title;
    [TextArea/*Multiline*/(15, 20)]
    public string Description;
    [Space(20)]
    public AudioClip Pastiche;
    public Sprite Background;
}
