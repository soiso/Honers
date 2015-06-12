using UnityEngine;
using System.Collections;

public class Panel : MonoBehaviour {

    public enum ROTATE_MODE
    {
        _X,
        _Y,
    }

    public PanelParametor m_param { get; private set; }
    private Vector3 m_target_Pos { get; set; }

    private float m_current_Leap;
    private ROTATE_MODE m_current_RotateMode;

    public Color m_debug_TimeZoneColor;

    private float m_target_Offset;
    private float m_current_Offset;
    private float m_changedir;

    private Material m_material;

    private float m_leap_Speed;

	void Start () 
    {
        m_param = GetComponent<PanelParametor>();
        m_current_Leap = 0f;
        m_material = GetComponent<MeshRenderer>().material;
        Calculate_TargetOffset(m_param.GetTimezone);
        m_current_Offset = m_target_Offset;
        m_leap_Speed = this.transform.parent.GetComponent<CommonPanelParametor>().m_leap_Speed;
	}

    void Calculate_TargetOffset(PanelParametor.TIMEZONE timezone)
    {
        switch (timezone)
        {
            case PanelParametor.TIMEZONE.morning:
                m_target_Offset = 0.0f;
                break;

            case PanelParametor.TIMEZONE.noon:
                m_target_Offset = 0.35f;
                break;

            case PanelParametor.TIMEZONE.night:
                m_target_Offset = 0.7f;
                break;

        }
    }

    void Calculate_UV()
    {
        MeshFilter filter = GetComponent<MeshFilter>();
        Mesh mesh = filter.mesh;

        if (m_current_Offset != m_target_Offset)
        {
            m_current_Offset += m_changedir / (this.transform.parent.GetComponent<PanelChanger>().m_ChangeSecond*60);
            if (m_changedir > 0 )
                m_current_Offset = Mathf.Clamp( m_current_Offset, m_current_Offset, m_target_Offset );
            else
                m_current_Offset = Mathf.Clamp(m_current_Offset, m_target_Offset, m_current_Offset);
        }
        else
        {
            if (m_param.m_isChangeNow)
            {
                m_param.Change_End();
                var changer = this.transform.parent.GetComponent<PanelChanger>();
                changer.Change_End();
            }
        }

        mesh.uv = new Vector2[]
        {
            new Vector2(m_current_Offset,.0f),
            new Vector2(m_current_Offset+0.125f,1.0f),
            new Vector2(m_current_Offset+0.125f,.0f),
            new Vector2(m_current_Offset,1.0f),
        };
    }

    public void Set_TargetUV( PanelParametor.TIMEZONE time )
    {
        if (m_param.m_isChangeNow)
            return;

        Calculate_TargetOffset(time);
        gameObject.GetComponent<PanelParametor>().Change_Timezone(time);
        m_changedir = m_target_Offset - m_current_Offset;
        m_param.Change_Begin();
    }

    public bool Begin_Move(Vector3 target_pos,ROTATE_MODE rotate_Mode)
    {
        if (m_param.m_isChangeNow)
            return false;
        
        m_target_Pos = target_pos;
        m_param.Change_Begin();
        m_current_Leap = 0f;
        m_current_RotateMode = rotate_Mode;
        return true;
    }

    private void End_Move()
    {
        m_param.Change_End();
        m_current_Leap = 0f;
        var changer =  this.transform.parent.GetComponent<PanelChanger>();
        changer.Change_End();
        transform.rotation = Quaternion.identity;
    }

    void    Move()
    {
        m_current_Leap += m_leap_Speed;
        Mathf.Clamp(m_current_Leap, 0, 1);
        this.transform.position = Vector3.Lerp(this.transform.position, m_target_Pos, m_current_Leap);
        Vector3 axis = new Vector3(0,0,0);
        if(m_current_RotateMode == Panel.ROTATE_MODE._X)
        {
            axis.x = 1;
        }
        else
        {
            axis.y = 1;
        }
        //角度の分割
        float work = (m_leap_Speed / 1.0f);
        float min_angle = (int)(360.0f * work);
        this.transform.Rotate(axis, min_angle);

       if(m_current_Leap >=1.0f)
       {
           m_param.Change_End();
           End_Move();
       }
    }

	void Update ()
    {
        //if (m_param.m_isChangeNow)
        //    Move();

        Calculate_UV();
	}

}
