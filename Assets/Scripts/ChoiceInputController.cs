using UnityEngine;

public class ChoiceInputController : MonoBehaviour
{
	private ChoiceController m_GameManager;

	private bool m_BlueChosen;
	private bool m_OrangeChosen;

	void Start()
	{
		m_GameManager = GetComponent<ChoiceController>();  // todo: shouldn't need to because the object is already a ChoiceController
	}

	// Update is called once per frame
	void Update()
	{
		if (!m_BlueChosen)
		{
			if (Input.GetKeyUp(KeyCode.A) || Input.GetButtonUp("BlueAggressive"))  // Blue is Aggressive
			{
				m_GameManager.SetChosen(true, 0);
			}
			else if (Input.GetKeyUp(KeyCode.D) || Input.GetButtonUp("BluePassive"))  // Blue is Passive
			{
				m_GameManager.SetChosen(true, 1);
			}
		}

		if (!m_OrangeChosen)
		{
			if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetButtonUp("OrangeAggressive"))  // Orange is Aggressive
			{
				m_GameManager.SetChosen(false, 0);
			}
			else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetButtonUp("OrangePassive"))  // Orange is Passive
			{
				m_GameManager.SetChosen(false, 1);
			}
		}
	}

    // todo: also needs a function for detemining controller type and displaying the relevant icons
}
