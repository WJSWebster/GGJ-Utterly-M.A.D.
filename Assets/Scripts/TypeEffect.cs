using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public enum Status
    {
        WaitingToBegin = 0,
        Typing = 1,
        Finished = 2,
    }

    [SerializeField]
    private bool BeginOnStart = false;
    public Status CurrStatus = Status.WaitingToBegin;
    [SerializeField]
    private AudioClip TypingSoundEffect;
    [SerializeField]
    private AudioSource Source;

    //[TextArea]
    [HideInInspector]
    public string TextString;
    [SerializeField]
    private float TypeSpeed;  // in secs

	private string PartialText;
	private float TotalTime;

	private Text Label;

    //void Awake()
    //{
    //}

    // Start is called before the first frame update
    void Start()
    {
        if(BeginOnStart)
        {
            Begin();
        }
    }

    public void Begin()
    {
        Label = GetComponent<Text>();
        TextString = Label.text;

        PartialText = "";
		TotalTime = 0f;

		Source.clip = TypingSoundEffect;
		Source.Play();

        CurrStatus = Status.Typing;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrStatus == Status.Typing)
        {
            TotalTime += Time.deltaTime;

            while (TotalTime >= TypeSpeed && PartialText.Length < TextString.Length)
            {
                PartialText += TextString[PartialText.Length];
                TotalTime -= TypeSpeed;
            }
            Label.text = PartialText;

            if (PartialText.Length >= TextString.Length)
            {
                Source.Stop();
                CurrStatus = Status.Finished;
            }
        }
    }
}
