using UnityEngine;
using System.Collections;

public class PlayerParametor : MonoBehaviour
{

    [SerializeField, Range(2f, 5f)]
    private float m_maxSpeed = 2;      //最高速度
    public float Get_MaxSpeed { get { return m_maxSpeed; } }
    [SerializeField, Range(0.1f, 0.5f)]
    private float m_acceleration = 0.001f;   //加速度
    public float Get_Acceleration { get { return m_acceleration; } }
    [SerializeField, Range(0.1f, 3f)]  //減速速さ
    private float m_brakeSpeed = 0.1f;
    public float Get_BrakeSpeed { get { return m_brakeSpeed; } }
    [SerializeField]
    private string hand_bonename = "hairband";
    public string Get_HandBone_Name { get { return hand_bonename; } }

    [SerializeField]
    private float m_stop_radius = 1.0f;
    public float Get_StopLength { get{return m_stop_radius;} }

}
