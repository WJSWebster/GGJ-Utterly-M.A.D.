using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using TMPro;

public class KeybindDialogBox : MonoBehaviour
{
    [SerializeField]
    InputManager m_InputManager;
    //[SerializeField]
    //GameObject m_KeyItemPrefab;
    [SerializeField]
    GameObject[] m_KeyList = new GameObject[4];
    string m_ButtonToRebind = null;
    Dictionary<string, TMP_Text> m_ButtonToLabel;

    // Start is called before the first frame update
    void Start()
    {
        //m_InputManager = FindObjectOfType<InputManager>();
        Debug.Assert(m_InputManager != null, "KeybindDialogBox::m_InputManger is unassigned!");

        // Create one key list item per button in m_InputManager
        string[] buttonNames = m_InputManager.GetButtonNames();
        m_ButtonToLabel = new Dictionary<string, TMP_Text>();

        // TODO: potentially remove all of this, because it assumes that keybindings are generated programmatically, when we've hard-coded to always have 4!
        //foreach(string bn in buttonNames)
        for(int i = 0; i < buttonNames.Length; i++)
        {
            string bn = buttonNames[i];

            /*GameObject go = Instantiate(m_KeyItemPrefab);
            go.transform.SetParent(m_KeyList.transform);
            go.transform.localScale = Vector3.one;*/

            // The label alongside the button itself, saying what action that button relates to
            /*Text buttonNameText = m_KeyList[i].transform.Find("Button Name").GetComponent<Text>();
            buttonNameText.text = bn;*/

            //Text keyNameText = m_KeyList[i].transform.Find("Keybind Button/Text").GetComponent<Text>();
            TMP_Text keyNameText = m_KeyList[i].transform.Find("Text").GetComponent<TextMeshProUGUI>();
            keyNameText.text = Sanitise(m_InputManager.GetKeyCodeForButton(bn));
            m_ButtonToLabel[bn] = keyNameText;

            //Button keyBindButton = m_KeyList[i].transform.Find("Keybind Button").GetComponent<Button>();
            Button keyBindButton = m_KeyList[i].GetComponent<Button>();
            keyBindButton.onClick.AddListener(() => { StartRebindFor(bn); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(m_ButtonToRebind != null)
        {
            if(Input.anyKeyDown)
            {
                // Loop through all possible keys and see if it was pressed down
                Array kcs = Enum.GetValues(typeof(KeyCode));

                foreach(KeyCode kc in kcs)
                {
                    if(Input./*GetKeyUp*/GetKeyDown(kc))
                    {
                        m_InputManager.SetButtonForKey(m_ButtonToRebind, kc);
                        m_ButtonToLabel[m_ButtonToRebind].text = Sanitise(kc);
                        // TODO: if kc.ToString() doesn't return a single char, intervene!
                        m_ButtonToRebind = null;
                        break;
                    }
                }
                // else, if not found in the 365 default keys...
            }
        }
    }

    string Sanitise(KeyCode KeyCode)
    {
        switch(KeyCode)
        {
            case KeyCode.Backspace:
                return "⌫";
            case KeyCode.Tab:
                return "⭾";
            case KeyCode.Return:
            case KeyCode.KeypadEnter:
                return "⏎";
            case KeyCode.Escape:
                return "␛";
            case KeyCode.Space:
                return "␣";
            case KeyCode.Exclaim:
                return "!";
            case KeyCode.DoubleQuote:
                return "\"";
            case KeyCode.Hash:
                return "#";
            case KeyCode.Dollar:
                return "$";
            case KeyCode.Percent:
                return "%";
            case KeyCode.Ampersand:
                return "&";
            case KeyCode.Quote:
                return "'";
            case KeyCode.LeftParen:
                return "(";
            case KeyCode.RightParen:
                return ")";
            case KeyCode.Asterisk:
            case KeyCode.KeypadMultiply:
                return "*";
            case KeyCode.Plus:
            case KeyCode.KeypadPlus:
                return "+";
            case KeyCode.Comma:
                return ",";
            case KeyCode.Minus:
            case KeyCode.KeypadMinus:
                return "-";
            case KeyCode.Period:
            case KeyCode.KeypadPeriod:
                return ".";
            case KeyCode.Slash:
            case KeyCode.KeypadDivide:
                return "/";
            case KeyCode.Alpha0:
            case KeyCode.Keypad0:
                return "0";
            case KeyCode.Alpha1:
            case KeyCode.Keypad1:
                return "1";
            case KeyCode.Alpha2:
            case KeyCode.Keypad2:
                return "2";
            case KeyCode.Alpha3:
            case KeyCode.Keypad3:
                return "3";
            case KeyCode.Alpha4:
            case KeyCode.Keypad4:
                return "4";
            case KeyCode.Alpha5:
            case KeyCode.Keypad5:
                return "5";
            case KeyCode.Alpha6:
            case KeyCode.Keypad6:
                return "6";
            case KeyCode.Alpha7:
            case KeyCode.Keypad7:
                return "7";
            case KeyCode.Alpha8:
            case KeyCode.Keypad8:
                return "8";
            case KeyCode.Alpha9:
            case KeyCode.Keypad9:
                return "9";
            case KeyCode.Colon:
                return ":";
            case KeyCode.Semicolon:
                return ";";
            case KeyCode.Less:
                return "<";
            case KeyCode.Equals:
            case KeyCode.KeypadEquals:
                return "=";
            case KeyCode.Greater:
                return ">";
            case KeyCode.Question:
                return "?";
            case KeyCode.At:
                return "@";
            case KeyCode.LeftBracket:
                return "[";
            case KeyCode.Backslash:
                return "\\";
            case KeyCode.RightBracket:
                return "]";
            case KeyCode.Caret:
                return "^";
            case KeyCode.Underscore:
                return "_";
            case KeyCode.BackQuote:
                return "`";
            case KeyCode.LeftCurlyBracket:
                return "{";
            case KeyCode.Pipe:
                return "|";
            case KeyCode.RightCurlyBracket:
                return "}";
            case KeyCode.Tilde:
                return "~"; // aka ~
            case KeyCode.Delete:
                return "␡";
            case KeyCode.UpArrow:
                return "↑";
            case KeyCode.DownArrow:
                return "↓";
            case KeyCode.RightArrow:
                return "→";
            case KeyCode.LeftArrow:
                return "←";
            case KeyCode.Insert:
                // todo
            case KeyCode.Home:
                // todo
            case KeyCode.End:
            // todo
            case KeyCode.PageUp:
            // todo
            case KeyCode.PageDown:
            // todo
            case KeyCode.Numlock:
            // todo
            case KeyCode.CapsLock:
            // todo
            case KeyCode.ScrollLock:
            // todo
            case KeyCode.RightShift:
            // todo
            case KeyCode.LeftShift:
            // todo
            case KeyCode.RightControl:
            // todo
            case KeyCode.LeftControl:
                return "𖡄";
            // todo
            case KeyCode.RightAlt:
            case KeyCode.LeftAlt:
                return "⎇";
            case KeyCode.RightCommand:  // aka KeyCode.RightApple
            // todo
            case KeyCode.LeftCommand:  // aka KeyCode.LeftApple
            // todo
            case KeyCode.LeftWindows:
            // todo
            case KeyCode.RightWindows:
            // todo
            case KeyCode.AltGr:
            // todo
            case KeyCode.Help:
                return "𖡄";
            // todo
            case KeyCode.Print:
                return "🖶";
            case KeyCode.SysReq:
            // todo
            case KeyCode.Break:
            // todo
            case KeyCode.Menu:
                return "𖡄";
            // todo
            case KeyCode.Mouse0:
            // todo
            case KeyCode.Mouse1:
            // todo
            case KeyCode.Mouse2:
            // todo
            case KeyCode.Mouse3:
            // todo
            case KeyCode.Mouse4:
            // todo
            case KeyCode.Mouse5:
            // todo
            case KeyCode.Mouse6:
                return "🖯";
            // todo
            case KeyCode.JoystickButton0:
            // todo
            case KeyCode.JoystickButton1:
            // todo
            case KeyCode.JoystickButton2:
            // todo
            case KeyCode.JoystickButton3:
            // todo
            case KeyCode.JoystickButton4:
            // todo
            case KeyCode.JoystickButton5:
            // todo
            case KeyCode.JoystickButton6:
            // todo
            case KeyCode.JoystickButton7:
            // todo
            case KeyCode.JoystickButton8:
            // todo
            case KeyCode.JoystickButton9:
            // todo
            case KeyCode.JoystickButton10:
            // todo
            case KeyCode.JoystickButton11:
            // todo
            case KeyCode.JoystickButton12:
            // todo
            case KeyCode.JoystickButton13:
            // todo
            case KeyCode.JoystickButton14:
            // todo
            case KeyCode.JoystickButton15:
            // todo
            case KeyCode.JoystickButton16:
            // todo
            case KeyCode.JoystickButton17:
            // todo
            case KeyCode.JoystickButton18:
            // todo
            case KeyCode.JoystickButton19:
            // todo
            case KeyCode.Joystick1Button0:
            // todo
            case KeyCode.Joystick1Button1:
            // todo
            case KeyCode.Joystick1Button2:
            // todo
            case KeyCode.Joystick1Button3:
            // todo
            case KeyCode.Joystick1Button4:
            // todo
            case KeyCode.Joystick1Button5:
            // todo
            case KeyCode.Joystick1Button6:
            // todo
            case KeyCode.Joystick1Button7:
            // todo
            case KeyCode.Joystick1Button8:
            // todo
            case KeyCode.Joystick1Button9:
            // todo
            case KeyCode.Joystick1Button10:
            // todo
            case KeyCode.Joystick1Button11:
            // todo
            case KeyCode.Joystick1Button12:
            // todo
            case KeyCode.Joystick1Button13:
            // todo
            case KeyCode.Joystick1Button14:
            // todo
            case KeyCode.Joystick1Button15:
            // todo
            case KeyCode.Joystick1Button16:
            // todo
            case KeyCode.Joystick1Button17:
            // todo
            case KeyCode.Joystick1Button18:
            case KeyCode.Joystick1Button19:
            case KeyCode.Joystick2Button0:
            case KeyCode.Joystick2Button1:
            case KeyCode.Joystick2Button2:
            case KeyCode.Joystick2Button3:
            case KeyCode.Joystick2Button4:
            case KeyCode.Joystick2Button5:
            case KeyCode.Joystick2Button6:
            case KeyCode.Joystick2Button7:
            case KeyCode.Joystick2Button8:
            case KeyCode.Joystick2Button9:
            case KeyCode.Joystick2Button10:
            case KeyCode.Joystick2Button11:
            case KeyCode.Joystick2Button12:
            case KeyCode.Joystick2Button13:
            case KeyCode.Joystick2Button14:
            case KeyCode.Joystick2Button15:
            case KeyCode.Joystick2Button16:
            case KeyCode.Joystick2Button17:
            case KeyCode.Joystick2Button18:
            case KeyCode.Joystick2Button19:
            case KeyCode.Joystick3Button0:
            case KeyCode.Joystick3Button1:
            case KeyCode.Joystick3Button2:
            case KeyCode.Joystick3Button3:
            case KeyCode.Joystick3Button4:
            case KeyCode.Joystick3Button5:
            case KeyCode.Joystick3Button6:
            case KeyCode.Joystick3Button7:
            case KeyCode.Joystick3Button8:
            case KeyCode.Joystick3Button9:
            case KeyCode.Joystick3Button10:
            case KeyCode.Joystick3Button11:
            case KeyCode.Joystick3Button12:
            case KeyCode.Joystick3Button13:
            case KeyCode.Joystick3Button14:
            case KeyCode.Joystick3Button15:
            case KeyCode.Joystick3Button16:
            case KeyCode.Joystick3Button17:
            case KeyCode.Joystick3Button18:
            case KeyCode.Joystick3Button19:
            case KeyCode.Joystick4Button0:
            case KeyCode.Joystick4Button1:
            case KeyCode.Joystick4Button2:
            case KeyCode.Joystick4Button3:
            case KeyCode.Joystick4Button4:
            case KeyCode.Joystick4Button5:
            case KeyCode.Joystick4Button6:
            case KeyCode.Joystick4Button7:
            case KeyCode.Joystick4Button8:
            case KeyCode.Joystick4Button9:
            case KeyCode.Joystick4Button10:
            case KeyCode.Joystick4Button11:
            case KeyCode.Joystick4Button12:
            case KeyCode.Joystick4Button13:
            case KeyCode.Joystick4Button14:
            case KeyCode.Joystick4Button15:
            case KeyCode.Joystick4Button16:
            case KeyCode.Joystick4Button17:
            case KeyCode.Joystick4Button18:
            case KeyCode.Joystick4Button19:
            case KeyCode.Joystick5Button0:
            case KeyCode.Joystick5Button1:
            case KeyCode.Joystick5Button2:
            case KeyCode.Joystick5Button3:
            case KeyCode.Joystick5Button4:
            case KeyCode.Joystick5Button5:
            case KeyCode.Joystick5Button6:
            case KeyCode.Joystick5Button7:
            case KeyCode.Joystick5Button8:
            case KeyCode.Joystick5Button9:
            case KeyCode.Joystick5Button10:
            case KeyCode.Joystick5Button11:
            case KeyCode.Joystick5Button12:
            case KeyCode.Joystick5Button13:
            case KeyCode.Joystick5Button14:
            case KeyCode.Joystick5Button15:
            case KeyCode.Joystick5Button16:
            case KeyCode.Joystick5Button17:
            case KeyCode.Joystick5Button18:
            case KeyCode.Joystick5Button19:
            case KeyCode.Joystick6Button0:
            case KeyCode.Joystick6Button1:
            case KeyCode.Joystick6Button2:
            case KeyCode.Joystick6Button3:
            case KeyCode.Joystick6Button4:
            case KeyCode.Joystick6Button5:
            case KeyCode.Joystick6Button6:
            case KeyCode.Joystick6Button7:
            case KeyCode.Joystick6Button8:
            case KeyCode.Joystick6Button9:
            case KeyCode.Joystick6Button10:
            case KeyCode.Joystick6Button11:
            case KeyCode.Joystick6Button12:
            case KeyCode.Joystick6Button13:
            case KeyCode.Joystick6Button14:
            case KeyCode.Joystick6Button15:
            case KeyCode.Joystick6Button16:
            case KeyCode.Joystick6Button17:
            case KeyCode.Joystick6Button18:
            case KeyCode.Joystick6Button19:
            case KeyCode.Joystick7Button0:
            case KeyCode.Joystick7Button1:
            case KeyCode.Joystick7Button2:
            case KeyCode.Joystick7Button3:
            case KeyCode.Joystick7Button4:
            case KeyCode.Joystick7Button5:
            case KeyCode.Joystick7Button6:
            case KeyCode.Joystick7Button7:
            case KeyCode.Joystick7Button8:
            case KeyCode.Joystick7Button9:
            case KeyCode.Joystick7Button10:
            case KeyCode.Joystick7Button11:
            case KeyCode.Joystick7Button12:
            case KeyCode.Joystick7Button13:
            case KeyCode.Joystick7Button14:
            case KeyCode.Joystick7Button15:
            case KeyCode.Joystick7Button16:
            case KeyCode.Joystick7Button17:
            case KeyCode.Joystick7Button18:
            case KeyCode.Joystick7Button19:
            case KeyCode.Joystick8Button0:
            case KeyCode.Joystick8Button1:
            case KeyCode.Joystick8Button2:
            case KeyCode.Joystick8Button3:
            case KeyCode.Joystick8Button4:
            case KeyCode.Joystick8Button5:
            case KeyCode.Joystick8Button6:
            case KeyCode.Joystick8Button7:
            case KeyCode.Joystick8Button8:
            case KeyCode.Joystick8Button9:
            case KeyCode.Joystick8Button10:
            case KeyCode.Joystick8Button11:
            case KeyCode.Joystick8Button12:
            case KeyCode.Joystick8Button13:
            case KeyCode.Joystick8Button14:
            case KeyCode.Joystick8Button15:
            case KeyCode.Joystick8Button16:
            case KeyCode.Joystick8Button17:
            case KeyCode.Joystick8Button18:
            case KeyCode.Joystick8Button19:
                return "🕹";
        }

        return KeyCode.ToString();
    }

    void StartRebindFor(string ButtonName)
    {
        m_ButtonToRebind = ButtonName;
    }
}
