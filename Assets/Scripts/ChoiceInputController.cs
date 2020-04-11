using UnityEngine;

public class ChoiceInputController : MonoBehaviour
{
    [SerializeField]
    private ChoiceController m_GameManager;
    [SerializeField]  // debug
	private InputManager m_InputManager;  // Todo: convert into a static singleton, so no object needs to be made

	private bool m_BlueChosen;
	private bool m_OrangeChosen;

	void Start()
	{
		m_GameManager = GetComponent<ChoiceController>();  // todo: shouldn't need to because the object is already a ChoiceController
        //GameObject.FindGameObjectsWithTag("InputManager")
        GameObject debug = GameObject.Find("EventSystem");
        m_InputManager = GameObject.Find("EventSystem").GetComponent<InputManager>();
        if(m_InputManager == null)
        {
            bool debug2 = true;
        }
	}

	// Update is called once per frame
	void Update()
	{
		if (!m_BlueChosen)
		{
			if (m_InputManager.GetButtonUp("BlueAggressive"))  // Blue is Aggressive
			{
				m_GameManager.SetChosen(true, 0);
			}
			else if (m_InputManager.GetButtonUp("BluePassive"))  // Blue is Passive
			{
				m_GameManager.SetChosen(true, 1);
			}
		}

		if (!m_OrangeChosen)
		{
			if (m_InputManager.GetButtonUp("OrangeAggressive"))  // Orange is Aggressive
			{
				m_GameManager.SetChosen(false, 0);
			}
			else if (m_InputManager.GetButtonUp("OrangePassive"))  // Orange is Passive
			{
				m_GameManager.SetChosen(false, 1);
			}
		}
	}

	// todo: also needs a function for determining controller type and displaying the relevant icons
}
