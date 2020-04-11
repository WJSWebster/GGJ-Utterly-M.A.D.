using UnityEngine;

public class RotateCurve : MonoBehaviour
{
    public AnimationCurve m_MoveCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public const float m_AnimationTime = 1f;
    public Transform m_HourArm;

    private Quaternion m_StartRot;
    private Quaternion m_Target;
    private float m_AnimationTimer;

    [SerializeField]
    private bool m_IsMainMenuCamera = false;

    public bool m_IsRotating;

    void Start()
    {
        if(m_HourArm == null)
        {
            m_HourArm = transform;
        }
    }

    void Update()
    {
        if(m_IsRotating)
        {
            if (m_AnimationTimer < m_AnimationTime)//m_Target != m_HourArm.localRotation)
            {
                m_AnimationTimer += Time.deltaTime;
                m_HourArm.localRotation = Quaternion.SlerpUnclamped(m_StartRot, m_Target, m_MoveCurve.Evaluate(m_AnimationTimer));
            }
            else
            {
                m_IsRotating = false;
                m_AnimationTimer = 0f;
            }
        }
    }

    public void SetNewRotation(Quaternion Destination, Quaternion? Origin = null)
    {
        m_StartRot = Origin.HasValue ? Origin.GetValueOrDefault() : m_HourArm.localRotation;
        m_Target = Destination;

        if(m_IsMainMenuCamera)
        {
            // Quaternion multiplication - https://forum.unity.com/threads/get-the-difference-between-two-quaternions-and-add-it-to-another-quaternion.513187/#post-3359650
            Quaternion delta = m_HourArm.localRotation * Quaternion.Inverse(m_StartRot);

            m_Target *= delta;
        }

        m_IsRotating = true;
    }

    public Quaternion GetTarget()
    {
        return m_Target;
    }
}