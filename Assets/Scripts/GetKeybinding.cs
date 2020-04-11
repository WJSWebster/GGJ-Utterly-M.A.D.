using UnityEngine;
using System.Collections;

public class GetKeybinding : MonoBehaviour
{
    // Keyboard polling: 9.504352E-06 s / frame
    // (as timed, avg of 1000 frames, profiler reports 0.00 ms)

    private int[] values;
    private bool[] keys;

    void Awake()
    {
        values = (int[])System.Enum.GetValues(typeof(KeyCode));
        keys = new bool[values.Length];
    }

    // https://forum.unity.com/threads/find-out-which-key-was-pressed.385250/#post-2505064
    void Update()
    {
        for (int i = 0; i < values.Length; i++)
        {
            keys[i] = Input.GetKey((KeyCode)values[i]);

            if(keys[i])
            {

                Debug.Log("Update::Detected System KeyCode: " + 
                    System.Enum.GetName(typeof(KeyCode), (KeyCode)values[i]));
            }
        }
    }

    // https://forum.unity.com/threads/find-out-which-key-was-pressed.385250/#post-2504577
    // Detects keys pressed and prints their keycode
    void OnGUI()
    {
        Event e = Event.current;
        if (e.isKey && Event.current.type == EventType.KeyUp)
        {
            Debug.Log("OnGUI::Detected pressed key code: " + e.keyCode);
        }
    }
}
