using UnityEngine;
using System;

public class TitleSine : MonoBehaviour
{
    [SerializeField]
    private bool m_UseCoSine;
    private float m_BasePosX;
    [SerializeField]
    private float m_PosXFrequency;
    [SerializeField]
    private float m_PosXRange;
    [Space(10)]

    private float m_BasePosY;
    [SerializeField]
    private float m_PosYFrequency;
    [SerializeField]
    private float m_PosYRange;
    [Space(10)]

    private float m_BaseScale;
    [SerializeField]
    private float m_ScaleFrequency;
    [SerializeField]
    private float m_ScaleRange;
    
    // Start is called before the first frame update
    void Start()
    {
        m_BasePosX = transform.localPosition.x;
        m_BasePosX = transform.localPosition.x;
        m_BaseScale = transform.localScale.x;  // note: because both the x and y scale are acting the same atm
    }

    // Update is called once per frame
    void Update()
    {
        //Action<float> action = m_UseCoSine ? (new Action<float>(Mathf.Sin)) : Mathf.Cos;
        //delegate float action(float  = new Action<float>(Mathf.Sin);

        float posX = m_UseCoSine ? Mathf.Cos(Time.time * m_PosXFrequency) : Mathf.Sin(Time.time * m_PosXFrequency);
        float posY = m_UseCoSine ? Mathf.Cos(Time.time * m_PosYFrequency) : Mathf.Sin(Time.time * m_PosYFrequency);
        transform.localPosition = new Vector3(m_BasePosX + (posX * m_PosXRange),
                                              m_BasePosY + (posY * m_PosYRange));

        float scale = m_BaseScale + (Mathf.Sin(Time.time * m_ScaleFrequency) * m_ScaleRange);
        transform.localScale = new Vector3(scale, scale);
    }
}
