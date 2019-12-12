using UnityEngine;

public class RotateCurve : MonoBehaviour
{
    public AnimationCurve m_MoveCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    public Transform m_HourArm;  // todo: figure out which one is actually needed? this or ClockFace.m_HourArm?

    private Quaternion m_StartRot;
    public Quaternion m_Target;  // todo: only public while trying to figure out how to check for time differences between clockface and this
    private float m_AnimationTimer;

    public bool m_IsRotating;

    //private void Start()
    //{
    //    SetNewRotation();
    //}

    private void Update()
    {
        if(m_IsRotating)
        {
            if (m_AnimationTimer < 1f)//m_Target != m_HourArm.localRotation)
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
        m_Target = Destination;//Random.insideUnitSphere;

        m_IsRotating = true;
    }
}