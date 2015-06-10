using UnityEngine;
using System.Collections;

public class Ranking : MonoBehaviour
{
    int[] m_Score;

	// Use this for initialization
	void Start ()
    {
        m_Score = new int[3];
        for( int i = 0; i < 3; i++ )
        {
            m_Score[i] = PlayerPrefs.GetInt("Rank" + (i+1).ToString(), 0);
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	    for( int i = 0; i < 3; i++ )
        {
            Debug.Log(m_Score[i]);
            PlayerPrefs.SetInt("Rank" + (i + 1).ToString(), m_Score[i]);
        }
	}

    //スコアとランキングを比較してランクインした順位を返す
    //ランク外なら-1
    public int  Compare( int score )
    {

        for( int i = 0; i < 3; i++ )
        {
            if( score > m_Score[i] )
            {
                for (int j = 2 - i; j > 0; j-- )
                {
                    m_Score[j] = m_Score[j - 1];
                }
                    return i;
            }
        }

        return -1;
    }
}
