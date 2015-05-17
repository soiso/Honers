using UnityEngine;
using System.Collections;

public class Fruit_Dongri : FruitInterFace
{

    [SerializeField]
    private float m_default_Score;
    [SerializeField]
    private float m_attenuation_Score;
    private PanelParametor.TIMEZONE m_current_Timezone;

    [SerializeField, HeaderAttribute("消えるまでの時間(秒)")]
    private float m_EraseTime = 10;

    [SerializeField, HeaderAttribute("EraseTime = EraseTime + Randam.Range(0, eraseAdjust)")]
    private float m_erase_Adjust = 10;

    [SerializeField, HeaderAttribute("点滅し始めるまでの時間")]
    private float m_swith_Time;

    [SerializeField, HeaderAttribute("点滅周期")]
    private float m_swith_Interval = 1.0f;

    [SerializeField, HeaderAttribute("点滅周期を狭める速度")]
    private float m_switch_adjust = 0.01f;

    private float m_current_switchInterval;
    private bool m_is_GetPlayer = false;
    private float m_nextSwitch;
    public AudioClip clip;
    private AudioSource sound;
    private ParticleSystem m_effect;

    //  private GameObject fruit_counter;
    void Start()
    {
        m_EraseTime = Time.time + m_EraseTime + Random.Range(0, m_erase_Adjust);
        m_current_switchInterval = m_swith_Interval;
        m_effect = GetComponent<ParticleSystem>();
    }

    void Sound_Check()
    {
        if (!sound.isPlaying)
            DestroyObject(gameObject);
    }

    void Update_Arive()
    {
        float arive_time = m_EraseTime - Time.time;
        if (arive_time < 0)
        {
            DestroyObject(gameObject);
        }

        if (arive_time < m_swith_Time)
        {
            float interval = Time.time + m_current_switchInterval;
            var renderer = GetComponent<MeshRenderer>();
            if (!renderer)
            {
                renderer = GetComponentInChildren<MeshRenderer>();
            }
            if (Time.time > m_nextSwitch)
            {
                renderer.enabled = !renderer.enabled;
                m_nextSwitch = Time.time + m_swith_Interval;
                m_swith_Interval -= m_switch_adjust;

                //sonoutinaosu
                m_default_Score = m_default_Score - 5f;
                if (m_default_Score < 0)
                    m_default_Score = 0;
            }
        }
    }

    void Update()
    {
        if (m_is_GetPlayer)
        {
            Sound_Check();
            return;
        }
        Update_Arive();

    }

    public override void Collision(GameObject col_object)
    {
        if (m_is_GetPlayer)
            return;

        var paper = this.transform.root.GetComponent<PicturePaper>();
        if (paper.m_move)
            return;

        sound = GetComponent<AudioSource>();
        sound.clip = clip;
        sound.Play();
        m_is_GetPlayer = true;

        var renderer = GetComponent<MeshRenderer>();
        if (!renderer)
        {
            renderer = GetComponentInChildren<MeshRenderer>();
        }

        renderer.enabled = false;

        var collider = GetComponents<BoxCollider>();
        foreach (BoxCollider it in collider)
        {
            it.enabled = false;
        }

        var s_collider = GetComponents<SphereCollider>();
        foreach (SphereCollider it in s_collider)
        {
            it.enabled = false;
        }
        Objectmanager.m_instance.m_fruit_Counter.Bagin_FeaverTime();
    }


}
