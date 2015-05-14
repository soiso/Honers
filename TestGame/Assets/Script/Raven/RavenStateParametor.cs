using UnityEngine;
using System.Collections;

public class RavenStateParametor : MonoBehaviour {

    [HeaderAttribute("とまり木にとまる時間(秒)")]
    public float m_wait_Time_Tomarigi = 5;

    [SerializeField, HeaderAttribute("とまり木にとまる確率"), Range(0, 100)]
    public int m_probability_Tomarigi = 50;

    [HideInInspector]
    public float m_current_WaitTime_Tomarigi = 0;

}
