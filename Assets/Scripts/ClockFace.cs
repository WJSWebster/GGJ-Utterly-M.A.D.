using UnityEngine;

public class ClockFace : MonoBehaviour
{
	[SerializeField]
	private Transform m_HourArm;

	//private bool m_IsChangingTime;
	//private int? m_OldHour;
	//[Range(9, 15)]
	public const int LowestStep = 0;
	public const int HighestStep = 6;
	[Range(LowestStep, HighestStep)]  // in total, 7 different steps
	public int m_Hour;

	private const float HourDivAmount = 15f;//30f;//22.5f;
	private RotateCurve m_RotateAnim;
	private ScoreKeeper m_ScoreKeeper;

	// Start is called before the first frame update
	void Start()
	{
		m_ScoreKeeper = GameObject.Find("ScoreKeeper").GetComponent<ScoreKeeper>();
		// TODO: m_Hour = m_ScoreKeeper.ClockHour
		//m_IsChangingTime = false;
		//m_HourArm.localRotation = CalcQuatAngle(m_HourArm.localRotation);

		m_RotateAnim = GetComponent<RotateCurve>();
		m_RotateAnim.m_HourArm = m_HourArm;

		Invoke("InitialTimeSet", 1f);
	}

	void InitialTimeSet()
	{
		UpdateTime(m_ScoreKeeper.m_Time, m_ScoreKeeper.m_PrevTime);
	}

	// should only be called by ScoreKeeper
	public void UpdateTime(int NewHour, int? OldHour = null)   // note: currently not used anywhere
	{
		m_Hour = NewHour;

		if(OldHour.HasValue)  // todo: couldn't figure out a way to just make this a simple ternary
		{
			m_RotateAnim.SetNewRotation(CalcQuatAngle(m_Hour), CalcQuatAngle(OldHour.GetValueOrDefault()));
		}
		else
		{
			m_RotateAnim.SetNewRotation(CalcQuatAngle(m_Hour));
		}

	}

	// Update is called once per frame
	void Update()
	{
		//if(m_IsChangingTime)
		//{
		Quaternion hourArmRot = CalcQuatAngle(m_Hour);
		if (!m_RotateAnim.m_IsRotating && (hourArmRot != m_RotateAnim.m_Target))
		{
			m_RotateAnim.SetNewRotation(hourArmRot);
		}

		//m_RotateAnim = GetComponent<RotateCurve>();
		//}
	}

	Quaternion CalcQuatAngle(int HourStep)
	{
		if(HourStep > HighestStep || HourStep < 0)
		{
			Debug.LogError("HourStep(" + HourStep + ") is outside of the acceptable range!");
		}

		return Quaternion.Euler(0f, 0f, -((HourStep - LowestStep) * HourDivAmount));
	}
}
