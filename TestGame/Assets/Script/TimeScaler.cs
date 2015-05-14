using UnityEngine;
using System.Collections;

public class TimeScaler : MonoBehaviour
{
    [SerializeField, HeaderAttribute("1が通常のスピード")]
    private float m_default_Timescale = 1.0f;
 
	
    public void Stop_Game()
    {
        Time.timeScale = 0f;
    }

    public void Return_Default_TimeScale()
    {
        Time.timeScale = m_default_Timescale;
    }

}
