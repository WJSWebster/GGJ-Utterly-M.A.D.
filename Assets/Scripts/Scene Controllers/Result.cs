using UnityEngine;

// TODO: this may not need to two classes, what is ResultRounds actually adding here?

[System.Serializable]  // lets you embed a class (Sound) with sub properties in the inspector within another component (Audio Manager)
public class IndvResult
{
    //public enum ChoiceCombinations { AggAgg, AggPass, PassAgg, PassPass, }
    //public readonly string m_ResultType;  // i.e. the title of this IndvResult

    [TextArea/*Multiline*/(15, 20)]
    public string m_TickerText;
    public bool m_IsAlien = false;
    public AudioClip m_Read;

    //public IndvResult(string ResultType)  // ctor
    //{
    //    m_ResultType = ResultType;
    //}
}

[System.Serializable]
public class ResultRounds
{
    public IndvResult[] m_Results = new IndvResult[4];

    //public ResultRounds()  // just to make this a little easier to parse in the inspector
    //{
        //m_Results[0] = new IndvResult("AggAgg");
        //m_Results[1] = new IndvResult("AggPass");
        //m_Results[2] = new IndvResult("PassAgg");
        //m_Results[3] = new IndvResult("PassPass");
    //}
}
