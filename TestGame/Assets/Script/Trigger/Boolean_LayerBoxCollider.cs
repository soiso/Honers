using UnityEngine;
using System.Collections;

public class Boolean_LayerBoxCollider : MonoBehaviour {

    public bool m_is_Active { get; private set; }

    [SerializeField]
    private string[] m_EnableLayer;

    void Start()
    {
        if(m_EnableLayer.Length == 0)
        {
            Debug.Log(this.gameObject.name + "EnableLayer is null !!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    bool    Is_Register(string layer_name)
    {
        for(int i = 0 ; i < m_EnableLayer.Length ; i++)
        {
            if (layer_name == m_EnableLayer[i])
                return true;
        }
        return false;
    }

    void OnTriggerEnter(Collider col_object)
    {
       string layername = LayerMask.LayerToName(col_object.gameObject.layer);
       if (Is_Register(layername))
            m_is_Active = true;
    }

    void OnTriggerStay(Collider col_object)
    {
        string layername = LayerMask.LayerToName(col_object.gameObject.layer);
        if (Is_Register(layername))
            m_is_Active = true;
    }

    void OnTriggerExit(Collider col_object)
    {
        string layername = LayerMask.LayerToName(col_object.gameObject.layer);
        if (Is_Register(layername))
            m_is_Active = true;
    }
}
