using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChoiceController : MonoBehaviour
{
    [Header("UI Components:")]
    [SerializeField]
    private Text m_Title;
    [SerializeField]
    private Text m_Description;

    [SerializeField]
    private TextController m_BlueText;
    [SerializeField]
    private TextController m_OrangeText;
    [Space(20)]

    [SerializeField]
    private Text m_BlueAggressiveBtn;
    [SerializeField]
    private Text m_BluePassiveBtn;
    [SerializeField]
    private Text m_OrangeAggressiveBtn;
    [SerializeField]
    private Text m_OrangePassiveBtn;
    [Space(20)]

    [SerializeField]
    private AudioClip m_BackgroundMusic;
    [SerializeField]
    private AudioSource m_BackgroundSource;
    [Space(20)]

    [SerializeField]
    private Round[] m_ChoiceRounds = new Round[4];  // todo: " "
    private Round m_CurrentRound;
    [Space(20)]

    [SerializeField]
    private AudioClip m_StampSoundEffect;
    [SerializeField]
    private AudioSource m_StampSource;
    [Space(10)]

    [SerializeField]
    private GameObject m_WallpaperPlane;
    [SerializeField]
    private Material m_WallpaperMat;
    [Space(10)]

    [SerializeField]
    private Animator m_Animator;

    [Header("Setup Values:")]
    private bool m_BlueChosen = false;
    private bool m_OrangeChosen = false;

    private ScoreKeeper m_ScoreKeeper;
    private bool m_NotYetCalculated = true;
    private int m_BlueChoice;  // O for Aggressive, 1 for Passive
    private int m_OrangeChoice;  // O for Aggressive, 1 for Passive

    private int m_SceneIncrement;  // 1 to 4  // TODO: don't use this either, as there's now only one Results scene, instead will need to call a function with this or something?


    void Start()
    {
        m_ScoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        m_CurrentRound = m_ChoiceRounds[m_ScoreKeeper.m_RoundNo.GetValueOrDefault()];

        m_Title.text = m_CurrentRound.Title;
        m_Description.text = m_CurrentRound.Description;

        m_StampSource.clip = m_StampSoundEffect;
        m_BackgroundSource.clip = m_BackgroundMusic;
        m_BackgroundSource.Play();

        m_BluePassiveBtn.text = m_CurrentRound.BluePassiveChoice;
        m_BlueAggressiveBtn.text = m_CurrentRound.BlueAggressiveChoice;
        m_OrangePassiveBtn.text = m_CurrentRound.OrangePassiveChoice;
        m_OrangeAggressiveBtn.text = m_CurrentRound.OrangeAggressiveChoice;

        if(m_WallpaperMat == null) // or m_WallpaperMat.Equals(emptyGameObject)
        {
            m_WallpaperMat = m_WallpaperPlane.GetComponent<Renderer>().GetComponent<Material>();

            Debug.Assert(m_WallpaperMat == null);
        }

        m_WallpaperMat.SetTexture("_BaseMap", m_CurrentRound.NormalMap);
        m_WallpaperMat.SetTexture("_SpecGlossMap", m_CurrentRound.Wallpaper);
        m_WallpaperMat.SetTexture("_EmissionMap", m_CurrentRound.Wallpaper);
    }

    public void SetChosen(bool IsBlue, int Choice)  // not: for Choice, 0 == aggressive, 1 == Passive
    {
        (IsBlue ? ref m_BlueChosen : ref m_OrangeChosen) = true;
        (IsBlue ? m_BlueText : m_OrangeText).SetPlanting();
        (IsBlue ? ref m_BlueChoice : ref m_OrangeChoice) = Choice;

        m_StampSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_NotYetCalculated && (m_BlueChosen && m_OrangeChosen))
        {
            m_NotYetCalculated = false;
            DeterminePointDistribution();

            Invoke(/*"MoveToReport"*/"FadeOut", 1f);
        }
    }

    void DeterminePointDistribution()
    {
        Debug.Log("We're in DeterminePointDistribution!");

        int bluePoints = 0;
        int orangePoints = 0;

        if (m_BlueChoice == 0 && m_OrangeChoice == 0)
        {
            bluePoints = m_CurrentRound.AggAgg[0];
            orangePoints = m_CurrentRound.AggAgg[1];

            m_SceneIncrement = 0;
        }
        else if (m_BlueChoice == 0 && m_OrangeChoice == 1)
        {
            bluePoints = m_CurrentRound.AggPass[0];
            orangePoints = m_CurrentRound.AggPass[1];

            m_SceneIncrement = 1;
        }
        else if (m_BlueChoice == 1 && m_OrangeChoice == 0)
        {
            bluePoints = m_CurrentRound.PassAgg[0];
            orangePoints = m_CurrentRound.PassAgg[1];

            m_SceneIncrement = 2;
        }
        else if (m_BlueChoice == 1 && m_OrangeChoice == 1)
        {
            bluePoints = m_CurrentRound.PassPass[0];
            orangePoints = m_CurrentRound.PassPass[1];

            m_SceneIncrement = 3;
        }
        else
        {
            Debug.Log("Determining Points has failed: \nSOMETHING HAS GONE HORRIBLY WRONG!");
        }

        if (m_ScoreKeeper != null)
        {
            m_ScoreKeeper.IncBothPlayersScore(bluePoints, orangePoints);
            m_ScoreKeeper.SetResultIndex(m_SceneIncrement);
        }
        else
        {
            Debug.Log("scoreKeeper not found!");
        }

        Debug.Log("SceneIncrement: " + m_SceneIncrement);
    }

    public static IEnumerator FadeOutAudio(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    void FadeOut()
    {
        StartCoroutine(FadeOutAudio(m_BackgroundSource, 0.5f));
        m_Animator.SetTrigger("FadeOut");
    }
}
