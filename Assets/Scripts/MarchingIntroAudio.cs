using UnityEngine;

public class MarchingIntroAudio : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_IntroSource;
    //[SerializeField]
    //private AudioClip m_Prelude;
    //[SerializeField]
    //private AudioSource m_IntroSource;
    [SerializeField]
    private AudioSource m_PreludeSource;

    [SerializeField]
    private AudioClip m_CariageReturn;

    // For whatever reason, this didn't work as intended any both clips ended up playing over one another
    /*void Awake()
    {
        m_PreludeSource.PlayScheduled(m_IntroSource.clip.length);
    }*/

    public void Update()
    {
        if (!m_PreludeSource.isPlaying && !m_IntroSource.isPlaying)
        {
            m_PreludeSource.Play();
        }
    }

    public void PlayCarriageReturn()
    {
        m_IntroSource.clip = m_CariageReturn;
        m_IntroSource.Play();
    }
}
