using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
    //private Text score_num;
    private NumberRenderer score_num;
    public float score;
    // Use this for initialization
    void Awake()
    {
        //score_num = GetComponentInChildren<Text>();
        score_num = GetComponentInChildren<NumberRenderer>();
    }
    void Start()
    {
        score = 0.0f;
    }
    // Update is called once per frame
    void Update()
    {
        //score_num.text = score.ToString();
       
    }
    public void SetScore(float num)
    {
        score += num;
        score_num.SetNumber((int)score);
    }
    public float GetScore()
    {
        return score;
    }
    public void Reset()
    {
        score = 0;
    }
}
