using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockFace : MonoBehaviour
{
    public GameObject hourArm;

	[Range(9, 12)]
	public int Hour;

	private float HourDivAmount = 30f;//22.5f;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		Quaternion hourTrans = hourArm.transform.localRotation;
		hourArm.transform.localRotation = Quaternion.Euler(hourTrans.x, 
                                                            hourTrans.y, 
                                                            -((Hour - 9) * HourDivAmount));
    }
}
