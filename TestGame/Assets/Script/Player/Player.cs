﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {

    private Animator m_animator;
    public Animator Get_Animator
    {
        get { if (!m_animator) m_animator = GetComponent<Animator>(); return m_animator; }
    }
    private Rigidbody m_rigidBody;
    public Rigidbody Get_RigidBody
    {
        get{if(!m_rigidBody) m_rigidBody = GetComponent<Rigidbody>(); return m_rigidBody;}
        //set{m_rigidBody = value;} 
    }

    public CharacterController m_controller { get; private set; }

    private PlayerStateMachine m_state_Machine = null;
    private PlayerFrameInformation m_frame_Information;
    public PlayerFrameInformation Get_FrameInfo
    {
        get { return m_frame_Information; }
    }
    private PlayerParametor m_param;
    public PlayerParametor Get_Param
    {
        get { return m_param; }
    }

    [SerializeField, HeaderAttribute("ダメージ食らったときの無敵時間 / 秒")]
    private float m_strong_second = 2;

    private float m_lift_strongTime =0;
    public bool m_is_strong { get; private set; }

    //private List<Fruit.FRUIT_TYPE> m_have_fruit;

    private Color m_debug_color = Color.white;
    [SerializeField]
    //private Text m_have_Fruittext;
    public Score m_score { get; private set; }

    public int m_StopFrame = 0;

    //private TimeZone_BoxCollider m_TimeZoneTrigger;
    //private PanelParametor.TIMEZONE m_TimeZone;

    public Vector3 m_lossyScale;

    [SerializeField, HeaderAttribute("落ちた時に復帰する場所")]
    private Transform m_SpornPoint;
    private bool m_isSporn = false;

    [SerializeField, HeaderAttribute("点滅周期")]
    private float m_swith_Interval = 1.0f;

    [SerializeField, HeaderAttribute("点滅時間")]
    private int m_swith_Time;
    private int m_switch_Timer = 0;
    private float m_nextSwitch;

    private ParticleSystem m_Particle;

	// Use this for initialization
	void Start () 
    {
        m_animator = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody>();
        m_state_Machine = GetComponent<PlayerStateMachine>();
        m_frame_Information = GetComponent<PlayerFrameInformation>();
        m_param = GetComponent<PlayerParametor>();
        m_controller = GetComponent<CharacterController>();
        //m_have_fruit = new List<Fruit.FRUIT_TYPE>();
        m_score = GetComponent<Score>();
        //m_TimeZoneTrigger = GetComponentInChildren<TimeZone_BoxCollider>();
        m_lossyScale = this.transform.lossyScale;
        m_is_strong = false;
        m_Particle = GetComponentInChildren<ParticleSystem>();
	}
	
    void    Update_Strong()
    {
        if (!m_is_strong)
            return;

        if(m_lift_strongTime < Time.time)
        {
            m_is_strong = false;
        }
    }

	// Update is called once per frame
	void Update () 
    {
        //m_TimeZone = m_TimeZoneTrigger.m_myColliderTimeZone;
        //GetComponent<ShaderChanger>().Change( m_TimeZone );

        if( this.transform.position.y < -5 )
        {
            this.transform.position = m_SpornPoint.position;
            m_isSporn = true;
            m_switch_Timer = m_swith_Time * 60;
        }

        switch(m_frame_Information.m_movetype)
        {
            case PlayerFrameInformation.MOVE_TYPE.frame :
                m_frame_Information.Create(transform);
                break;

            case PlayerFrameInformation.MOVE_TYPE.slider :
                m_frame_Information.Create_WithSlider();
                break;
        }

        
	    if(m_state_Machine.Execute())
        {
            m_state_Machine.ReturnDefaultState();
        }
        //var child = transform.GetChild(1).transform;
        //for(int i = 0 ; i <child.childCount ; i++)
        //{
        //    var renderer = child.GetChild(i).GetComponent<SkinnedMeshRenderer>();
        //    renderer.material.color = m_debug_color;
        //}
        //zは固定
       // Vector3 pos = this.transform.position;
        //pos.z = 0;
       // this.transform.position = pos;
        Update_Strong();

        if(Time.timeScale > 0)
        {
            Vector3 pos = this.transform.position;
            pos.z = 0;
            this.transform.position = pos;
        }

        if( m_isSporn )
        {
            if( m_switch_Timer > 0 )
            {
                if (Time.time > m_nextSwitch)
                {
                    SkinnedMeshRenderer[] renderere = GetComponentsInChildren<SkinnedMeshRenderer>();
                    foreach (SkinnedMeshRenderer r in renderere)
                    {
                        r.enabled = !r.enabled;
                    }
                    m_nextSwitch = Time.time + m_swith_Interval;
                }
                m_switch_Timer--;
            }
            else
            {
                SkinnedMeshRenderer[] renderere = GetComponentsInChildren<SkinnedMeshRenderer>();
                foreach (SkinnedMeshRenderer r in renderere)
                {
                    if( !r.enabled ) r.enabled = !r.enabled;
                }
                m_isSporn = false;
            }
        }
	}

    public bool ChangeState(PlayerStateInterFace new_state)
    {
        return m_state_Machine.Change_State(new_state);
    }

     void OnTriggerEnter(Collider col_object)
    {
         //レイヤー名取得
        string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
         if(layer_name == "Panel")
         {
             m_debug_color = col_object.GetComponent<Panel>().m_debug_TimeZoneColor;
             var obj = col_object.GetComponent<Panel>();
             var param = col_object.GetComponent<PanelParametor>();
             m_debug_color = obj.m_debug_TimeZoneColor;
         }

         if(layer_name == "FieldFruit")
         {
             var fruit = col_object.GetComponent<FruitInterFace>();
             if (fruit == null) return;
             fruit.Collision(this.gameObject);
             m_Particle.Play();
         }

    }

    void    OnTriggerStay(Collider col_object)
     {
         //レイヤー名取得
         string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
         if (layer_name == "Panel")
         {
             var obj = col_object.GetComponent<Panel>();
             var param = col_object.GetComponent<PanelParametor>();
             m_debug_color = obj.m_debug_TimeZoneColor;

         }
        
     }

    void    OnTriggerExit(Collider col_object)
    {
        //レイヤー名取得
        string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
        if (layer_name == "Panel")
        {
            m_debug_color = col_object.GetComponent<Panel>().m_debug_TimeZoneColor;
            var obj = col_object.GetComponent<Panel>();
            var param = col_object.GetComponent<PanelParametor>();
            m_debug_color = obj.m_debug_TimeZoneColor;
        }
    }


   //public void Add_Score(float default_Score,Fruit.FRUIT_TYPE type)
   // {
   //     m_score.AddScore(default_Score);
   // }

   // public void Sub_Score( float defalut_Score)
   // {
   //     m_score.SubScore(defalut_Score);
   // } 

    public void Damage(DamageTrigger.DamageObject damage_Info)
    {
        if(m_is_strong)
            return;
         m_is_strong = true;
         m_lift_strongTime = Time.time + m_strong_second;

        //ここでモーション遷移
    }
   }

