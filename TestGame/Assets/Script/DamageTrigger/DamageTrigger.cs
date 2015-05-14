using UnityEngine;
using System.Collections;

public class DamageTrigger : MonoBehaviour
{
    //とりあえず6月末まで持てばいい
    public enum DAMAGE_TYPE
    {
        _default,
    }
    public class DamageObject
    {
        public DAMAGE_TYPE m_type;
        
        public DamageObject(DAMAGE_TYPE type)
        {
            m_type = type;
        }
    }

    [SerializeField, HeaderAttribute("ダメージ判定をとるCollision")]
    private Collider m_myCollision;

    private bool m_Hit = false;
    public bool IsHit() { return m_Hit; }

    private string m_HitName;
    public string GetHitName() { return m_HitName; }

    [SerializeField,HeaderAttribute("ダメージの種類")]
    private DAMAGE_TYPE m_damage_type;

    // Use this for initialization
    void Start()
    {
        m_myCollision.isTrigger = true;
        m_myCollision.enabled = false;
    }

    //public void SetPosition(Vector3 pos)
    //{
    //    //m_Collider.transform.position = pos;
    //}
    //
    //public void SetScale(Vector3 scale)
    //{
    //    //m_Collider.transform.localScale = scale;
    //}

    public void OnCollisionBegin()
    {
        m_myCollision.enabled = true;
        Debug.Log("DamageTriggerBegin!");
    }

    public void onCollisionEnd()
    {
        m_myCollision.enabled = false;
        Debug.Log("DamageTriggerEnd!");
    }

    void OnTriggerEnter(Collider col_obj)
    {
        Debug.Log("Hit");
        string layer_name = LayerMask.LayerToName(col_obj.gameObject.layer);

        DamageObject info = new DamageObject(m_damage_type);
        if (layer_name == "Player")
        {
            col_obj.gameObject.GetComponent<Player>().Damage(info);
        }
        else if (layer_name == "FieldObject")
        {
            if (col_obj.GetComponent<FieldObjectInterface>() == null)
                col_obj.gameObject.GetComponentInParent<FieldObjectInterface>().Damage(info);
            else
                col_obj.gameObject.GetComponent<FieldObjectInterface>().Damage(info);
        }
        m_Hit = true;
    }

    void OnTriggerStay(Collider col_obj)
    {
        Debug.Log("Hit!");
        string layer_name = LayerMask.LayerToName(col_obj.gameObject.layer);
        DamageObject info = new DamageObject(m_damage_type);
        if (layer_name == "Player")
        {
            col_obj.gameObject.GetComponent<Player>().Damage(info);
        }
        else if (layer_name == "FieldObject")
        {
            var tes = col_obj.gameObject.GetComponent("FieldObjectInterface") as FieldObjectInterface;
            if(tes)
            {
                tes.Damage(info);
            }
            
        }
        m_Hit = true;
        m_HitName = layer_name;
    }

    void OnTriggerExit(Collider col_obj)
    {
        //Debug.Log("Hit!");
        //string layer_name = LayerMask.LayerToName(col_obj.gameObject.layer);
        //if (layer_name == "Player")
        //{
        //    col_obj.gameObject.SendMessage("isRemove");
        //}
        //else if (layer_name == "FieldObject")
        //{
        //    col_obj.gameObject.SendMessage("isRemove");
        //}
        m_Hit = false;
    }

}