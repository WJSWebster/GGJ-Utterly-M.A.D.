using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.Color;
using System.Collections;
using TMPro;

//[RequireComponent(typeof(Text))]
public class TextFadeEffect : MonoBehaviour {

    private float m_BaseOpacity;          // The original scale of the title
    // private float m_BaseRot;            // The original (X) rotation of the title
    // private float m_BasePosX;           // The original X position of the title

    public float m_BobFrequency = 2f;   // The frequency of the scale change
    public float m_BobRange = 0.075f;   // The range of the scale change
    
    // private TextMeshPro Text;
    public Text Text;

    private void Start()
    {
        // Text = GetComponent<TextMeshPro>();
        Text = GetComponent<Text>();
        m_BaseOpacity = Text.color.a;
        // m_BaseRot   = transform.rotation.x;
        // m_BasePosX  = transform.position.x;
    }

    void Update ()
    {
        // core calculation, using sin wave to generate a change over time
        float fadeAnimation  = m_BaseOpacity + Mathf.Sin(Time.time * m_BobFrequency) * m_BobRange;
        
        // setting relevant transform variable
        Text.color = new Color(Text.color.r, Text.color.g, Text.color.b, fadeAnimation);
        
    }
}