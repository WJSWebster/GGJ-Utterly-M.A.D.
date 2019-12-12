using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
	[SerializeField]
	private Stamp[] m_Messages;
	[Space(20)]
	[SerializeField]
	private AudioSource m_Source;
	[SerializeField]
	private float m_PlantSpeed = 1.5f;  // in seconds
	[SerializeField]
	private Transform m_HoverTrans;

	private float m_PercentageDone = 0f;
	private Vector3 m_PlantPos;
	private Quaternion m_PlantRot;
	private Vector3 m_PlantScale;
	public bool m_IsPlanting = false;
	// public Transform debugPlantPlace = transform;

	// Start is called before the first frame update
	void Start()
	{
		// PlantTrans = /*GetComponent<Transform>().*/transform;
		m_PlantPos = transform.position;
		m_PlantRot = transform.rotation;
		m_PlantScale = transform.localScale;

		int randomNo = Random.Range(0, m_Messages.Length - 1);  // DEBUG
		Stamp randStamp = m_Messages[randomNo];

		GetComponent<Text>().text = randStamp.Text;
		m_Source.clip = randStamp.Voice;

		transform.position = m_HoverTrans.position;
		transform.rotation = m_HoverTrans.rotation;
		transform.localScale = m_HoverTrans.localScale;
	}

	// Update is called once per frame
	void Update()
	{
		if(m_IsPlanting)
		{
			if (m_PercentageDone < 1f)
			{
				m_PercentageDone += Time.deltaTime * m_PlantSpeed;

				transform.position = Vector3.Lerp(m_HoverTrans.position, 
												m_PlantPos, 
												m_PercentageDone);
				transform.rotation = Quaternion.Lerp(m_HoverTrans.rotation, 
												m_PlantRot, 
												m_PercentageDone);
				transform.localScale = Vector3.Lerp(m_HoverTrans.localScale, 
												m_PlantScale, 
												m_PercentageDone);
			}
			else
			{
				Debug.Log("Reached!");
				m_IsPlanting = false;
			}

		}
	}

	public void SetPlanting()
	{
		m_IsPlanting = true;

		Invoke("PlayVoice", .5f);
	}

	void PlayVoice()
	{
		m_Source.Play();
	}
}
