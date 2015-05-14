using UnityEngine;
using System.Collections;

public class FrogAttack : MonoBehaviour
{

    private Vector3 m_MoveDir;
    private float m_CurrentLerp = .0f;

    private float m_Speed;

    private DamageTrigger m_Trigger;

    [SerializeField, HeaderAttribute("生存時間(仮)")]
    private int m_Life;

    [SerializeField, HeaderAttribute("ダメージの種類")]
    private DamageTrigger.DAMAGE_TYPE m_damage_type;

	// Use this for initialization
	void Start () 
    {
        //m_Trigger = GetComponent<DamageTrigger>();
	}

    void FixedUpdate()
    {
        Rigidbody rigid = GetComponent<Rigidbody>();
        rigid.velocity = m_MoveDir * m_Speed;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //m_Trigger.OnCollisionBegin();

        if (this.transform.root.GetComponent<PicturePaper>().m_move)
            return;

        m_Life--;
        if( m_Life < 0 )
        {
            GameObject.Find("Frog").GetComponentInChildren<Frog>().SubWater();
            DestroyObject(this.gameObject);
        }

	}

    void OnTriggerEnter( Collider col_obj)
    {
        //m_Trigger.onCollisionEnd();
        string layer_name = LayerMask.LayerToName(col_obj.gameObject.layer);
        DamageTrigger.DamageObject info = new DamageTrigger.DamageObject(m_damage_type);
        if (layer_name == "Player")
        {
            col_obj.gameObject.GetComponent<Player>().Damage(info);
        }
        else if (layer_name == "FieldObject")
        {
            var tes = col_obj.gameObject.GetComponent("FieldObjectInterface") as FieldObjectInterface;
            if (tes)
            {
                tes.Damage(info);
            }

        }

        GameObject.Find("Frog").GetComponentInChildren<Frog>().SubWater();
        DestroyObject(this.gameObject);
    }

    public void SetParam( Vector3 dir, float speed )
    {
        m_MoveDir = dir.normalized;
        m_Speed = speed;
    }
}
