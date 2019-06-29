using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Title;
    [SerializeField]
    private GameObject Description;

    [SerializeField]
    private GameObject BlueText;
    [SerializeField]
    private GameObject OrangeText;

    private TextController BlueTextController;
    private TextController OrangeTextController;

    [SerializeField]
    private GameObject BluePassiveBtn;
    [SerializeField]
    private GameObject BlueAggressiveBtn;
    [SerializeField]
    private GameObject OrangePassiveBtn;
    [SerializeField]
    private GameObject OrangeAggressiveBtn;

    private bool BlueChosen = false;
    private bool OrangeChosen = false;

    [SerializeField]
    private Round CurrentRound;

    [SerializeField]
    private AudioClip StampSoundEffect;
    [SerializeField]
	private AudioSource Source;

    private bool NotYetCalculated = true;
    private int BlueChoice;  // O for Aggressive, 1 for Passive
    private int OrangeChoice;  // O for Aggressive, 1 for Passive

    private int SceneIncrement;  // 1 to 4

    // Start is called before the first frame update
    void Start()
    {
        Title.GetComponent<Text>().text = CurrentRound.ScenerioName;
        Description.GetComponent<Text>().text = CurrentRound.ScenerioDesc;

        Source.clip = StampSoundEffect;

        BlueTextController = BlueText.GetComponent<TextController>();
        OrangeTextController = OrangeText.GetComponent<TextController>();

        BluePassiveBtn.transform.GetChild(0).GetComponent<Text>().text = CurrentRound.BluePassiveChoice;
        BlueAggressiveBtn.transform.GetChild(0).GetComponent<Text>().text = CurrentRound.BlueAggressiveChoice;
        OrangePassiveBtn.transform.GetChild(0).GetComponent<Text>().text = CurrentRound.OrangePassiveChoice;
        OrangeAggressiveBtn.transform.GetChild(0).GetComponent<Text>().text = CurrentRound.OrangeAggressiveChoice;
    }

    // Update is called once per frame
    void Update()
    {
        if(!BlueChosen)
        {
            if (Input.GetKeyUp(KeyCode.D) 
                || Input.GetButtonUp("BlueAggressive"))
            {
                Debug.Log("Blue is AGGRESSIVE!");
                BlueChosen = true;
                BlueTextController.IsPlanting = true;
                BlueChoice = 0;

		        Source.Play();
            }
            else if (Input.GetKeyUp(KeyCode.A) 
                || Input.GetButtonUp("BluePassive"))
            {
                Debug.Log("Blue is PASSIVE!");
                BlueChosen = true;
                BlueTextController.IsPlanting = true;
                BlueChoice = 1;

		        Source.Play();
            }
        }
		
        if(!OrangeChosen)
        {
            if (Input.GetKeyUp(KeyCode.RightArrow) 
                || Input.GetButtonUp("OrangeAggressive"))
            {
                Debug.Log("Orange is AGGRESSIVE!");
                OrangeChosen = true;
                OrangeTextController.IsPlanting = true;
                OrangeChoice = 0;

		        Source.Play();
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) 
                || Input.GetButtonUp("OrangePassive"))
            {
                Debug.Log("Orange is PASSIVE!");
                OrangeChosen = true;
                OrangeTextController.IsPlanting = true;
                OrangeChoice = 1;

                // yield return new WaitForSeconds(2f);
                Source.Play();
            }
        }

        if(NotYetCalculated && (BlueChosen && OrangeChosen))
        {
            NotYetCalculated = false;
            DeterminePointDistribution();
            ExecuteAfterTime(2f); // wait two seconds then advance scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + SceneIncrement);
        }
    }
    
    void DeterminePointDistribution()
    {
        Debug.Log("We're in DeterminePointDistribution!");
        
        int bluePoints = 0;
        int orangePoints = 0;

        if(BlueChoice == 0 && OrangeChoice == 0)
        {
            bluePoints = CurrentRound.AggAgg[0];
            orangePoints = CurrentRound.AggAgg[1];

            SceneIncrement = 1;
        }
        else if(BlueChoice == 0 && OrangeChoice == 1)
        {
            bluePoints = CurrentRound.AggPass[0];
            orangePoints = CurrentRound.AggPass[1];

            SceneIncrement = 2;
        }
        else if(BlueChoice == 1 && OrangeChoice == 0)
        {
            bluePoints = CurrentRound.PassAgg[0];
            orangePoints = CurrentRound.PassAgg[1];

            SceneIncrement = 3;
        }
        else if(BlueChoice == 1 && OrangeChoice == 1)
        {
            bluePoints = CurrentRound.PassPass[0];
            orangePoints = CurrentRound.PassPass[1];

            SceneIncrement = 4;
        }
        else
        {
            Debug.Log("Determing Points has failed: \nSOMETHING HAS GONE HORRIBLY WRONG!");
        }

        GameObject _ScoreKeeper = GameObject.Find("ScoreKeeper");
        if(_ScoreKeeper != null)
        {
            _ScoreKeeper.GetComponent<ScoreKeeper>().IncBothPlayersScore(bluePoints, orangePoints);
        }
        else
        {
            Debug.Log("scoreKeeper not found!");
        }
    }
    
    IEnumerator ExecuteAfterTime(float time)
    {
        Debug.Log("We're in Execute after time!");
        // DeterminePointDistribution();

        yield return new WaitForSeconds(time);

    }
}
