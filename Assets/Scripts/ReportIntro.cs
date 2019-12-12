using UnityEngine;

public class ReportIntro : MonoBehaviour
{
    [SerializeField]
    private ClockFace m_ClockFace;
    [Space(10)]

    [SerializeField]
    private AudioClip m_GongSound;
    [SerializeField]
    private AudioSource m_GongSource;
    [Space(20)]

    [SerializeField]
    private ResultController m_ResultController;

    private bool m_AreTimesDifferent;

    public void PlayGongSound()
    {
        if (m_AreTimesDifferent)    // if times are different - (else the hand wont be moving so don't make a gong noise)
        {
            m_GongSource.clip = m_GongSound;
            m_GongSource.Play();
        }
    }

    public void UpdateTime()
    {
        ScoreKeeper scoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
        int newTime = scoreKeeper.m_Time;
        m_AreTimesDifferent = newTime != scoreKeeper.m_PrevTime;
        
        m_ClockFace.UpdateTime(newTime);  // simply won't do anything if times aren't different, so no need to check here
    }

    public void BeginReport()
    {
        m_ResultController.BeginReport();
    }
}
