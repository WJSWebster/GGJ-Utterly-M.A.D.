using UnityEngine;

public class MainMenuConstRotate : MonoBehaviour
{
    [SerializeField]
    private float m_Speed;

    //// Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        //Vector3 eulerRot = new Vector3(0f, m_Speed, 0f);
        transform.Rotate(0f, m_Speed, 0f);
    }
}
