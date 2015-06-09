using UnityEngine;
using System.Collections;

public class PlayerParametor : MonoBehaviour
{
    [SerializeField, Range(2f, 7f),HeaderAttribute("最高速度")]
    private float m_maxSpeed = 2;   
    public float Get_MaxSpeed { get { return m_maxSpeed; } }
    [SerializeField, Range(0.1f, 0.5f), HeaderAttribute("加速スピード")]
    private float m_acceleration = 0.001f; 
    public float Get_Acceleration { get { return m_acceleration; } }
    [SerializeField, Range(0.1f, 3f), HeaderAttribute("減速スピード")] 
    private float m_brakeSpeed = 0.1f;
    public float Get_BrakeSpeed { get { return m_brakeSpeed; } }

    [SerializeField, Range(2,10),HeaderAttribute("どんぐり込の最大速度")]
    float m_donguri_maxSpeed;

    [SerializeField]
    private float m_stop_radius = 1.0f;
    public float Get_StopLength { get{return m_stop_radius;} }

    public void Add_PlayerMaxSpeed(float val)
    {
        m_maxSpeed += val;
        if (m_maxSpeed > m_donguri_maxSpeed)
            m_maxSpeed = m_donguri_maxSpeed;

    }

    public void Add_Acceleraoin(float val)
    {
        m_acceleration += val;
    }

    public void Add_BrakeSpeed(float val)
    {
        m_brakeSpeed += val;
    }
}
