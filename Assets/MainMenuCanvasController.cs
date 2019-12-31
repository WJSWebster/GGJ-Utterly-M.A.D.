using UnityEngine;

public class MainMenuCanvasController : MonoBehaviour  // a re-appropriated version of RotateCurve, but for Vector3s instead of Quaternions
{
    [SerializeField]
    private GameObject m_MainScreen;
    [SerializeField]
    private GameObject m_HowToScreen;
    [SerializeField]
    private GameObject m_SettingsScreen;
    [Space(10)]
    [SerializeField]
    private AnimationCurve m_MoveCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    //[Space(10)]
    //[SerializeField]
    //private RotateCurve m_RotateCurve;

    private Vector3 m_MainPos;
    private Vector3 m_HowToPos;
    private Vector3 m_SettingsPos;

    private bool m_IsRotating;
    private Vector3 m_StartPos;
    private Vector3 m_TargetPos;
    private const float m_AnimationTime = 1f;
    private float m_AnimationTimer;

    // Start is called before the first frame update
    void Start()
    {
        //if(m_RotateCurve == null)
        //{
        //    m_RotateCurve = GetComponent<RotateCurve>();
        //}
        m_MainPos = transform.position;
        float canvasWidth = GetComponent<RectTransform>().sizeDelta.x;

        m_HowToPos = m_HowToScreen.transform.position;
        float xScale = m_SettingsScreen.transform.lossyScale.x;
        m_HowToPos.x += -(canvasWidth * xScale);
        m_HowToScreen.transform.position = m_HowToPos;

        m_SettingsPos = m_SettingsScreen.transform.position;
        xScale = m_SettingsScreen.transform.lossyScale.x;
        m_SettingsPos.x += (canvasWidth * xScale);
        m_SettingsScreen.transform.position = m_SettingsPos;

        // todo: may also need to resize screens, assuming this isn't done automatically?
    }

    void Update()
    {
        if (m_IsRotating)
        {
            if (m_AnimationTimer < m_AnimationTime)
            {
                m_AnimationTimer += Time.deltaTime;
                transform.position = Vector3.LerpUnclamped(m_StartPos, m_TargetPos, m_MoveCurve.Evaluate(m_AnimationTimer));
            }
            else
            {
                m_IsRotating = false;
                m_AnimationTimer = 0f;
            }
        }
    }

    public void ShiftUI(Options Option)  // else Is Settings screen
    {
        m_StartPos = transform.position;
        m_TargetPos = Option == Options.Settings ? m_HowToPos   // investigate: why is this the wrong way round?
                                                 : Option == Options.HowTo ? m_SettingsPos 
                                                                           : m_MainPos;
        m_IsRotating = true;
    }
}
