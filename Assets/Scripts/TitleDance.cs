using UnityEngine;

public class TitleDance : MonoBehaviour
{
    [SerializeField]
    private float m_Tempo;
    [SerializeField]
    private AnimationCurve m_MoveCurve;
    [SerializeField]
    private Transform m_FirstPos;
    private Transform m_FirstPosSnap;  // snapshot of position
    private bool m_FirstNotSet = true;
    [SerializeField]
    private Transform m_SecondPos;
    private Transform m_SecondPosSnap;  // " "
    private bool m_SecondNotSet = true;

    [SerializeField]  // DEBUG
    private float m_Timer;

    // Start is called before the first frame update
    void Start()
    {
        m_Timer = 0f;
    }

    void Update()
    {
        m_Timer += Time.deltaTime / m_Tempo;
        float animCurve;

        if (m_Timer <= /*m_Tempo*/0.5f /*/ 2*/)
        {
            if (m_FirstNotSet)
            {
                m_FirstPosSnap = m_FirstPos;
                m_SecondPosSnap = m_SecondPos;

                m_FirstNotSet = false;
                m_SecondNotSet = true;
            }

            animCurve = m_MoveCurve.Evaluate(m_Timer * 2f);
            transform.localPosition = Vector3.Lerp(m_FirstPosSnap.localPosition, m_SecondPosSnap.localPosition, animCurve);
            transform.localRotation = Quaternion.Lerp(m_FirstPosSnap.localRotation, m_SecondPosSnap.localRotation, animCurve);
        }
        else if (m_Timer <= 1f/*m_Tempo*/)
        {
            if(m_SecondNotSet)
            {
                m_SecondPosSnap = m_SecondPos;
                m_FirstPosSnap = m_FirstPos;

                m_SecondNotSet = false;
                m_FirstNotSet = true;
            }

            animCurve = m_MoveCurve.Evaluate((m_Timer -.5f) * 2f);
            transform.localPosition = Vector3.Lerp(m_SecondPosSnap.localPosition, m_FirstPosSnap.localPosition, animCurve);
            transform.localRotation = Quaternion.Lerp(m_SecondPosSnap.localRotation, m_FirstPosSnap.localRotation, animCurve);
        }
        else
        {
            m_Timer = 0;
        }
    }
}
