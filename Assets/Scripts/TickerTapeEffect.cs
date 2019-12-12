using UnityEngine;

public class TickerTapeEffect : MonoBehaviour
{
    [SerializeField]
    private bool m_CanScroll = true;  // is there even any other place where this is used? if not, what is the point of this?
    [SerializeField]
    private float m_ScrollSpeed;
    [SerializeField]
    private float m_BeginTextBuffer;

    private Canvas m_Canvas;

    // Start is called before the first frame update
    void Start()
    {
        m_Canvas = GetComponentInParent<Canvas>();
		// transform.position = Vector3(Screen.width, transform.position.y, transform.position.z);
        Vector3 localPos = transform.localPosition;
        Rect rect = m_Canvas.transform.GetComponent<RectTransform>().rect;

        localPos.x = (transform.GetComponent<RectTransform>().rect.width / 2) + rect.xMax + m_BeginTextBuffer;//rect.x + rect.width;//Screen.width;
        transform.localPosition = localPos;
    }

    public void BeginScroll()
    {
        m_CanScroll = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_CanScroll)
        {
            Vector3 localPos = transform.localPosition;
            localPos.x -= (Time.deltaTime * m_ScrollSpeed);
            transform.localPosition = localPos;
        }
    }
}
