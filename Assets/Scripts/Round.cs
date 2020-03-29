using UnityEngine;

[System.Serializable]  // lets you embed a class (Sound) with sub properties in the inspector within another component (Audio Manager)
public class Round
{
    public string Title;
    [TextArea/*Multiline*/(15, 20)]
    public string Description;
    [Space(20)]

    [Header("Player Choice Text:")]
    public string BluePassiveChoice;
    public string BlueAggressiveChoice;
    public string OrangePassiveChoice;
    public string OrangeAggressiveChoice;
    
    [Header("Player Scores:")]
    [Tooltip("Rows: Blue | Orange")]
    public int[] AggAgg = new int[2];
    public int[] AggPass = new int[2];
    public int[] PassAgg = new int[2];
    public int[] PassPass = new int[2];
    [Space(20)]

    public Sprite Background;  // if we ever end up using this
    public Texture2D Wallpaper;
    public Texture2D NormalMap;
}