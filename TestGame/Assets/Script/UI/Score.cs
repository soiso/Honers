using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
    //private Text score_num;
    private NumberRenderer score_num;
    private float[] score = new float[5];
    private float total_score;
    // Use this for initialization
    void Awake()
    {
        //score_num = GetComponentInChildren<Text>();
        score_num = GetComponentInChildren<NumberRenderer>();
    }
    void Start()
    {
        Reset();
    }
    // Update is called once per frame
    void Update()
    {
        //score_num.text = score.ToString();
    }
    public void SetScore(float num, int stage)
    {
        score[stage] += num;
        total_score += num;
        score_num.SetNumber((int)score[stage]);
    }
    public float GetScore(int stage)
    {
        return score[stage];
    }
    public float GetTotalScore()
    {
        return total_score;
    }
    public void Reset()
    {
        for (int i = 0; i < 5; i++)
        {
            score[i] = .0f;
        }
        total_score = .0f;
    }
    public void Init()
    {
        score_num.SetNumber(0);
    }
}
