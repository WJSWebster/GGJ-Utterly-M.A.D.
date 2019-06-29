using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TickerTapeEffect : MonoBehaviour
{
    public float ScrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
		// transform.position = Vector3(Screen.width, transform.position.y, transform.position.z);
        Vector3 localPos = transform.localPosition;
		localPos.x = Screen.width;
        transform.localPosition = localPos;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 localPos = transform.localPosition;
        localPos.x -= (Time.deltaTime * ScrollSpeed);
        transform.localPosition = localPos;
    }
}
