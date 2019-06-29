using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField]
    private string[] Messages;
	[SerializeField]
	private float PlantSpeed = 1.5f;  // in seconds
	[SerializeField]
	private Transform HoverTrans;

    private float PercentageDone = 0f;  // **FOR DEBUG**
	private Vector3 PlantPos;  // **DEBUG**
	private Quaternion PlantRot;  // **DEBUG**
	private Vector3 PlantScale;  // **DEBUG**
	public bool IsPlanting = false;  // **FOR DEBUG**
    // public Transform debugPlantPlace = transform;

    // Start is called before the first frame update
    void Start()
    {
		// PlantTrans = /*GetComponent<Transform>().*/transform;
        PlantPos = transform.position;
        PlantRot = transform.rotation;
        PlantScale = transform.localScale;

		GetComponent<Text>().text = Messages[Random.Range(0, Messages.Length)];

        transform.position = HoverTrans.position;
        transform.rotation = HoverTrans.rotation;
        transform.localScale = HoverTrans.localScale;
    }

    // Update is called once per frame
    void Update()
    {
		if(IsPlanting)
        {
            if (PercentageDone < 1f)
            {
                PercentageDone += Time.deltaTime * PlantSpeed;

                transform.position = Vector3.Lerp(HoverTrans.position, 
                                                PlantPos, 
                                                PercentageDone);
                transform.rotation = Quaternion.Lerp(HoverTrans.rotation, 
                                                PlantRot, 
                                                PercentageDone);
                transform.localScale = Vector3.Lerp(HoverTrans.localScale, 
                                                PlantScale, 
                                                PercentageDone);
            }
            else
            {
                Debug.Log("Reached!");
                IsPlanting = false;
            }

        }
    }
}
