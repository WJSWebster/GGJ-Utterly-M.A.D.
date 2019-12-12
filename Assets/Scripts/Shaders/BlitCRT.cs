using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class BlitCRT : MonoBehaviour
{
    public Material m_EffectMaterial;
    public float m_SmoothRefresh;
    public float m_SmoothDistort;
    public float m_Interval;
    private float m_RefreshP;
    private float m_Distortion;
    private float m_sD;

    void Start()
    {
        m_RefreshP = 1080.0f;
        m_sD = m_SmoothDistort;
        StartCoroutine(Distort());
    }

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        m_EffectMaterial.SetFloat("_ScanPoint", m_RefreshP);
        m_EffectMaterial.SetFloat("_Distort", m_Distortion);

        if (m_EffectMaterial != null)
            Graphics.Blit(src, dst, m_EffectMaterial);
    }

    void FixedUpdate()
    {
        m_RefreshP = Mathf.MoveTowards(m_RefreshP, -200.0f, m_SmoothRefresh);
        if (m_RefreshP <= -200.0f)
        {
            m_RefreshP = 2000.0f;
        }
    }

    IEnumerator Distort()
    {
        float current = 0.0f;
        float target = NormalRandom(-m_Interval, m_Interval);
        m_Distortion = current;
        while (true)
        {
            current = Mathf.MoveTowards(current, target, m_sD * Time.deltaTime);
            m_Distortion = current;
            if (current == target)
            {
                yield return new WaitForSeconds(Random.Range(0.0f, 0.1f));
                target = NormalRandom(-m_Interval, m_Interval);
            }
            yield return null;
        }
    }

    float NormalRandom(float min, float max)
    {
        m_sD = m_SmoothDistort * Random.Range(0.2f, 1.0f);
        return Random.Range(min, max);
    }
}