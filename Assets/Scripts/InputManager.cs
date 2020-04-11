using UnityEngine;
using System.Collections.Generic;
using System.Linq;

// Acts as an interface for the ButtonKeys dictionary
public class InputManager : MonoBehaviour
{
    Dictionary<string, KeyCode> m_ButtonKeys;

    void OnEnable()
    {
        m_ButtonKeys = new Dictionary<string, KeyCode>();

        // TODO: consider reading these from a user preferences file
        m_ButtonKeys["BlueAggressive"] = KeyCode.A;
        m_ButtonKeys["BluePassive"] = KeyCode.D;
        m_ButtonKeys["OrangeAggressive"] = KeyCode.LeftArrow;
        m_ButtonKeys["OrangePassive"] = KeyCode.RightArrow;
    }

    // Start is called before the first frame update
    void /*Start*/Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("InputManager");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public bool GetButtonUp(string ButtonName)
    {
        if(m_ButtonKeys.ContainsKey(ButtonName) == false)
        {
            Debug.LogError("InputManger::GetButtonUp -- No Button named: " + ButtonName);
            return false;
        }

        return Input.GetKeyDown(m_ButtonKeys[ButtonName]);
    }

    public string[] GetButtonNames()
    {
        return m_ButtonKeys.Keys.ToArray();
    }

    public string GetKeyNameForButton(string ButtonName)
    {
        if (m_ButtonKeys.ContainsKey(ButtonName) == false)
        {
            Debug.LogError("InputManger::GetKeyNameForButton -- No Button named: " + ButtonName);
            return "N/A";
        }

        return m_ButtonKeys[ButtonName].ToString();
    }

    public KeyCode GetKeyCodeForButton(string ButtonName)
    {
        if (m_ButtonKeys.ContainsKey(ButtonName) == false)
        {
            Debug.LogError("InputManger::GetKeyCodeForButton -- No Button named: " + ButtonName);
            return KeyCode.None;
        }

        return m_ButtonKeys[ButtonName];
    }

    public void SetButtonForKey(string ButtonName, KeyCode KeyCode)
    {
        m_ButtonKeys[ButtonName] = KeyCode;
    }
}
