using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
    private Text score_num;
    public float score;
    // Use this for initialization
    void Awake()
    {
        score_num = GetComponentInChildren<Text>();
    }
    void Start()
    {
        score = 0;
    }
    // Update is called once per frame
    void Update()
    {
        score_num.text = score.ToString();
    }
    public void SetScore(float num)
    {
        score += num;
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
