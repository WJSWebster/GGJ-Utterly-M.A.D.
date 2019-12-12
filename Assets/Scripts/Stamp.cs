using UnityEngine;

[System.Serializable]  // lets you embed a class (Sound) with sub properties in the inspector within another component (Audio Manager)
public class Stamp
{
    public string Text;

    public AudioClip Voice;
}