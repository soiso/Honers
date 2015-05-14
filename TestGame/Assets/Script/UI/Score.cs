using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour
{
    private Text score_num;
    public int score;
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
    public void SetScore(int num)
    {
        score = num;
    }
}
