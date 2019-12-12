using UnityEngine;
using UnityEngine.UI;

public class SetupController : MonoBehaviour
{
    [Header("UI Components:")]
    [SerializeField]
    private Text m_Title;
    [SerializeField]
    private GameObject m_Description;
    private TypeEffect m_DescTypeEffect;
    [SerializeField]
    private AudioSource m_Source;
    [SerializeField]
    private TextFadeEffect m_PressSomethingTextFade;
    [Space(10)]

    [Header("Setup Values:")]
    [Range(0, /*m_SetupRounds.Length*/3)]
    public int m_RoundNo;  // from 0..3    (Setups.Length + 1)
    [SerializeField]
    private Setup[] m_SetupRounds = new Setup[4];

    // Others:
    private ScoreKeeper m_ScoreKeeper;
    private bool FadingEnabled = false;

    void Start()
    {
        m_ScoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();

        Setup currSetup = m_SetupRounds[m_ScoreKeeper.m_RoundNo.GetValueOrDefault()];

        m_Title.text = currSetup.Title;
        m_Description.GetComponent<Text>().text = currSetup.Description;
        m_DescTypeEffect = m_Description.GetComponent<TypeEffect>();
        m_DescTypeEffect.Begin();
        m_Source.clip = currSetup.Pastiche;
        m_Source.Play();
        m_PressSomethingTextFade.SetTextOpacity(0f);  // if it wasn't already
        m_PressSomethingTextFade.enabled = false;
    }

    void Update()
    {
        if (!FadingEnabled && m_DescTypeEffect.CurrStatus == TypeEffect.Status.Finished)  // to save on continuous, expensive, checks
        {
            m_PressSomethingTextFade.enabled = true;
            FadingEnabled = true;
        }
    }
}
