using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    [SerializeField]
    public int? m_RoundNo;  // todo: this actually only wants to be private w/ pub getter
    //{ get; private set; }

    [SerializeField]
    public int m_BluePlayerScore;
    [SerializeField]
    public int m_OrangePlayerScore;

    public int? m_ResultIndex  // what results screen to show: 0-AggAgg, 1-AggPass, 2-PassAgg, 3-PassPass
    { get; private set; }

    public int m_PrevTime  // from 0 to 6
    { get; private set; }
    public int m_Time  // from 0 to 6
    { get; private set; }

    // Not yet used:
    private IndvResult[] Log;  // not the final type, as we also need to log other details like roundno at the time

    // Start is called before the first frame update
    void Start()  // maybe this should be awake?
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("ScoreKeeper");  // ("Player")

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        if(!m_RoundNo.HasValue)  // i.e. if this is the first time this is instantiated
        {
            m_RoundNo = 0;
            m_BluePlayerScore = m_OrangePlayerScore = 0;

            m_Time = m_PrevTime = 0;
        }
    }

    public void IncBothPlayersScore(int BlueVal, int OrangeVal)
    {
        Debug.Log("We're in ScoreKeeper!");
        IncPlayersScore(true, BlueVal);
        IncPlayersScore(false, OrangeVal);

        CalcTime();
    }

    public void IncPlayersScore(bool IsBluePlayer, int Value)
    {
        (IsBluePlayer ? ref m_BluePlayerScore : ref m_OrangePlayerScore) += Value;
    }

    private void CalcTime()
    {
        int scoreDifference = Mathf.Abs(m_BluePlayerScore - m_OrangePlayerScore);

        //scoreDifference = Mathf.Min(scoreDifference, 50);  // the cap
        m_PrevTime = m_Time;
        m_Time = Mathf.Min(Mathf.RoundToInt(scoreDifference / 8.3f), /*HighestStep*/6);
    }

    public void SetResultIndex(int Index)
    {
        if (Index >= 0 && Index <= 3)
        {
            m_ResultIndex = Index;
            return;
        }

        Debug.Log("Error: Result Index tried to be set to " + Index + "!");
    }

    public void ResetResultIndex()
    {
        m_ResultIndex = null;
    }
}
