using UnityEngine;
using System.Collections;

public class Rank : MonoBehaviour
{

    [SerializeField, Range(0, 9997), HeaderAttribute("BとAの境")]
    public int score_sort_AB;
    [SerializeField, Range(0, 9999), HeaderAttribute("AとSの境")]
    public int score_sort_SA;
    [SerializeField, HeaderAttribute("ドラムロールする時間")]
    private float m_rollTime = 0.5f;
    [SerializeField]
    public Texture[] score_tex;

    private float m_rollBeginTime;
    private bool m_is_rolling = false;

    private int m_current_surplus = 0;
    private Material m_currentMaterial = null;
    private int Score = 0;
    void Awake()
    {
        m_currentMaterial = this.GetComponent<Renderer>().material;
        if (score_sort_AB > score_sort_SA)
            score_sort_SA = score_sort_AB + 2;
    }
    void Update()
    {
        if (m_is_rolling)
        {
            if (Dram_Roll())
            {
                Calculate();
            }
        }
    }
    bool Dram_Roll()
    {
        SetMaterialTexture(score_tex[Random.Range(0, 3)]);

        if (Time.time > m_rollBeginTime + m_rollTime)
        {
            m_is_rolling = false;
            return true;
        }
        return false;
    }
    void Calculate()
    {
        if (Score < score_sort_AB)
            SetMaterialTexture(score_tex[0]);
        if (Score > score_sort_AB && Score < score_sort_SA)
            SetMaterialTexture(score_tex[1]);
        if (Score > score_sort_SA)
            SetMaterialTexture(score_tex[2]);
    }

    public void Disalble()
    {
        this.GetComponent<Renderer>().enabled = false;
    }

    public void Enable()
    {
        this.GetComponent<Renderer>().enabled = true;
        m_rollBeginTime = Time.time;
        m_is_rolling = true;
    }

    public void SetMaterialTexture(Texture insert)
    {
        m_currentMaterial.mainTexture = insert;
        this.GetComponent<Renderer>().material = m_currentMaterial;
    }
    public void SetScore(int score)
    {
        Score = score;
    }
}
