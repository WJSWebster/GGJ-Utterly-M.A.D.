using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultController : MonoBehaviour
{
	[SerializeField]
	private Text m_Title;
	[SerializeField]
	private Text m_TickerTape;

	[Space(20)]
	[SerializeField]
	private Font m_HumanFont;
	[SerializeField]
	private Font m_AlienFont;

	[Space(20)]
	[SerializeField]
	private ResultRounds[] m_Results = new ResultRounds[4];

	[Space(20)]
	[SerializeField]
	private AudioSource m_ReadSource;
    [SerializeField]
    private AudioClip m_StaticSound;

	private ScoreKeeper m_ScoreKeeper;
	private IndvResult m_Result;
    private bool m_HasReportBegan;
    private bool m_HasStaticBegan;

    [SerializeField]
    private TickerTapeEffect m_TickerTapeEffect;
    [SerializeField]
    private GameObject m_StaticEffect;

    // Start is called before the first frame update
    void Awake()
	{
		m_ScoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();

        //m_StaticEffect.SetActive(false);  // disable static effect for now
        m_StaticEffect.GetComponent<Image>().enabled = false;

        m_Result = m_Results[m_ScoreKeeper.m_RoundNo.GetValueOrDefault()].m_Results[m_ScoreKeeper.m_ResultIndex.GetValueOrDefault()];

		// set title and description text and font
		m_TickerTape.text = m_Result.m_TickerText;
		m_Title.font = m_TickerTape.font = m_Result.m_IsAlien ? m_AlienFont : m_HumanFont;

		m_ReadSource.clip = m_Result.m_Read;
	}

    public void BeginReport()
    {
        m_ReadSource.Play();
        m_TickerTapeEffect.BeginScroll();

        m_HasReportBegan = true;
    }

    public void BeginStatic()
    {
        //m_StaticEffect.SetActive(true);
        m_StaticEffect.GetComponent<Image>().enabled = true;
        m_ReadSource.clip = m_StaticSound;

        m_ReadSource.Play();

        m_HasStaticBegan = true;
    }

    // Update is called once per frame
    void Update()
	{
		if(m_HasReportBegan && m_ReadSource.isPlaying == false)
		{
            if(m_HasStaticBegan)
            {
                if (m_ScoreKeeper.m_RoundNo < m_Results.Length - 1)
                {
                    // TODO: add m_ScoreKeeper's resultindex to the activity log first
                    m_ScoreKeeper.ResetResultIndex();

                    m_ScoreKeeper.m_RoundNo++;
                    SceneManager.LoadScene(2);

                    return;
                }

                SceneManager.LoadScene(5);  // load credits scene
            }
            else
            {
                BeginStatic();
            }
		}
	}
}
