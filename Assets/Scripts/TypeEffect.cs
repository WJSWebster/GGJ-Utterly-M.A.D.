using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public AudioClip TypingSoundEffect;
	public AudioSource Source;

	[TextArea]
	public string TextString;
	public float TypeSpeed;  // in secs

	private string PartialText;
	private float TotalTime;

	private Text Label;

    void Awake()
    {
		Label = GetComponent<Text>();
    }

    // Start is called before the first frame update
    void Start()
	{
		PartialText = "";
		TotalTime = 0f;

		Source.clip = TypingSoundEffect;
		Source.Play();
	}

    // Update is called once per frame
    void Update()
    {
		TotalTime += Time.deltaTime;

		while(TotalTime >= TypeSpeed && PartialText.Length < TextString.Length)
		{
			PartialText += TextString[PartialText.Length];
            TotalTime -= TypeSpeed;
        }
        Label.text = PartialText;

		if(PartialText.Length >= TextString.Length)
		{
			Source.Stop();
		}
    }
}
