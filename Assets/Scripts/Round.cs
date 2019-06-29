using UnityEngine;

[System.Serializable]  // lets you embed a class (Sound) with sub properties in the inspector within another component (Audio Manager)
public class Round
{
    [Range(1,4)]
    public int RoundNo;
    
    public string ScenerioName;
	[TextArea]
	public string ScenerioDesc;

    [Space(20)]

    [Header("Player Choices:")]
    public string OrangePassiveChoice;
    public string OrangeAggressiveChoice;
    public string BluePassiveChoice;
    public string BlueAggressiveChoice;

    [SerializeField]
    // private PlayerScores m_PlayerScores;
    [Header("Player Scores:")]
    [Tooltip("Rows: Blue | Orange \n Columns: Win/Win | Win/Lose | Lose/Win | Lose/Lose")]
    public int[] AggAgg = new int[2];
    public int[] AggPass = new int[2];
    public int[] PassAgg = new int[2];
    public int[] PassPass = new int[2];

    [Space(20)]

    public AudioClip BackgroundMusic;

    // a value that we populate in AudioManager, so hide from inspector
    [HideInInspector]
    public AudioSource source;
}