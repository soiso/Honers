using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LayerContainer_Collider : MonoBehaviour {

    public delegate void test();

    public List<GameObject> m_active_list{get; private set;}
    [SerializeField]
    private string[] m_EnableLayer;

    public List<GameObject> m_exit_List;

    public test m_tes;
    
	// Use this for initialization
	void Start () {
	    m_active_list = new List<GameObject>();
        m_exit_List = new List<GameObject>();
        if (m_EnableLayer.Length == 0)
        {
            Debug.Log(this.gameObject.name + "EnableLayer is null !!");
        }
  
	}

	
	// Update is called once per frame
	void Update () {
	
	}


    bool Is_Register(string layer_name)
    {
        for (int i = 0; i < m_EnableLayer.Length; i++)
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
        {
            if(!m_active_list.Contains(col_object.gameObject))
            {
                m_active_list.Add(col_object.gameObject);
            }
        }
            
    }

    void OnTriggerStay(Collider col_object)
    {
        string layername = LayerMask.LayerToName(col_object.gameObject.layer);
        if (Is_Register(layername))
        {
            if (!m_active_list.Contains(col_object.gameObject))
            {
                m_active_list.Add(col_object.gameObject);
            }
        }
    }

    void OnTriggerExit(Collider col_object)
    {
        string layername = LayerMask.LayerToName(col_object.gameObject.layer);
        if (Is_Register(layername))
        {
            if (m_active_list.Contains(col_object.gameObject))
            {
                m_active_list.Remove(col_object.gameObject);
                m_exit_List.Add(col_object.gameObject);
            }
        }
    }
}
