using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField]
    private int BluePlayerScore;
    [SerializeField]
    private int OrangePlayerScore;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void IncBothPlayersScore(int BlueVal, int OrangeVal)
    {
        Debug.Log("We're in ScoreKeeper!");
        IncBluePlayersScore(BlueVal);
        IncOrangePlayersScore(OrangeVal);
    }

    public void IncBluePlayersScore(int value)
    {
        BluePlayerScore += value;
    }
    
    public void IncOrangePlayersScore(int value)
    {
        OrangePlayerScore += value;
    }
}

