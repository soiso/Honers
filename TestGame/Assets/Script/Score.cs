using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    [SerializeField]
    Text m_text;
    private float m_current_Score = 0;
    private float m_target_Score = 0;
    [SerializeField]
    private float m_score_UpSpeed = 1;



	// Use this for initialization
	void Start () 
    {
	    if(m_text == null)
        {
            Debug.Log("Score Text is Null! !");
        }

	}
	
    public void AddScore(float add_score)
    {
        m_target_Score += add_score;
    }

    public void SubScore( float sub_score)
    {
        m_target_Score -= sub_score;
        if (m_target_Score < 0) m_target_Score = 0;
    }

	// Update is called once per frame
	void Update () 
    {
	    if(m_current_Score < m_target_Score)
        {
            m_current_Score += m_score_UpSpeed;
        }
        else if( m_current_Score > m_target_Score)
        {
            m_current_Score -= m_score_UpSpeed;
        }
       int drawScore = (int)m_current_Score;
       //m_text.text = "SCORE : " + drawScore.ToString();
	}
}
