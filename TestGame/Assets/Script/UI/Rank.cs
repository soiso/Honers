using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Rank : MonoBehaviour
{
    [SerializeField, Range(0, 10),HeaderAttribute("ロールが開始するまでの待ち時間")]
    private float start_wait=0;
    [SerializeField, Range(0, 9997), HeaderAttribute("BとAの境")]
    public int score_sort_AB;
    [SerializeField, Range(0, 9999), HeaderAttribute("AとSの境")]
    public int score_sort_SA;
    [SerializeField, HeaderAttribute("ドラムロールする時間")]
    private float m_rollTime = 0.5f;
    [SerializeField]
    public Sprite[] score_tex;

    private float m_start_time;
    private float m_rollBeginTime;
    private bool m_is_rolling = false;

    private int m_current_surplus = 0;
    private SpriteRenderer m_MainSpriteRenderer = null;
    private int Score = 0;
    private Vector3 rank_scale;
    void Awake()
    {
        m_MainSpriteRenderer = this.GetComponent<SpriteRenderer>();
        if (score_sort_AB > score_sort_SA)
            score_sort_SA = score_sort_AB + 2;
    }
    void Start()
    {
        m_start_time = Time.time;
    }
    void Update()
    {
        if (Time.time > m_start_time + start_wait)
        {
            if (m_is_rolling)
            {
                if (Dram_Roll())
                {
                    Calculate();
                }
            }
        }
    }
    bool Dram_Roll()
    {
        SetMaterialTexture(score_tex[Random.Range(0, 2)]);
        //rank_scale.x = Mathf.Lerp(rank_scale.x, 1.0f, 0.01f);
        //rank_scale.y = Mathf.Lerp(rank_scale.y, 1.0f, 0.01f);
        //this.transform.localScale = rank_scale;

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
        this.GetComponent<Image>().enabled = false;
    }

    public void Enable()
    {
        this.GetComponent<Image>().enabled = true;
        m_rollBeginTime = Time.time;
        m_is_rolling = true;
        //rank_scale = new Vector3(2.0f, 2.0f, 0.0f);
        //this.transform.localScale = rank_scale;
    }

    public void SetMaterialTexture(Sprite insert)
    {
        m_MainSpriteRenderer.sprite = insert;
    }
    public void SetScore(int score)
    {
        Color col = m_MainSpriteRenderer.color;
        m_MainSpriteRenderer.color = new Color(col.r,col.g,col.b,255);
        Score = score;
    }
}
