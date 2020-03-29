using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TextFadeEffect : MonoBehaviour
{
    [SerializeField]
    private bool m_IsFlashing = true;
    [SerializeField]
    private float m_BaseOpacity = 0.5f;  // The original opacity of the text
    [SerializeField]
    private float m_BobFrequency = 2f;   // The frequency of the opacity change
    [SerializeField]
    private float m_BobRange = 0.5f;   // The range of the opacity change

    [SerializeField]
    private Text m_Text;
    
    private float m_Red;
    private float m_Green;
    private float m_Blue;

    [SerializeField]  // debug
    public float m_Opacity;


    void Start()
    {
        if(m_Text == null)
            m_Text = GetComponent<Text>();

        m_Red = m_Text.color.r;
        m_Green = m_Text.color.g;
        m_Blue = m_Text.color.b;

        if (!m_IsFlashing)
        {    
            m_Text.enabled = false;
        }

        m_Opacity = m_Text.color.a;
    }

    void Update ()
    {
        if(m_IsFlashing)
        {
            // core calculation, using sin wave to generate a change over time
            float opacity = m_Opacity = m_BaseOpacity + (Mathf.Sin(Time.time * m_BobFrequency) * m_BobRange);

            // setting relevant transform variable
            SetTextOpacity(opacity);
        }
    }

    public void SetTextOpacity(float Opacity)
    {
        //Debug.Log("arg: " + Opacity);
        m_Text.color = new Color(m_Red, m_Green, m_Blue, Opacity);

        //Debug.Log("color: " + m_Text.color.a);
    }

    public void BeginFlashing()
    {
        m_Text.enabled = true;
        m_IsFlashing = true;
    }
}