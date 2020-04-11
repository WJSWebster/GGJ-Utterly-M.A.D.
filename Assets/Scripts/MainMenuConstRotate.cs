using UnityEngine;

public class MainMenuConstRotate : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, m_Speed, 0f);
    }
}
