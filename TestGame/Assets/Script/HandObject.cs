using UnityEngine;
using System.Collections;

public class HandObject : MonoBehaviour {

    private bool m_player_is_Range = false;
    private bool m_click_Me = false;
    private bool m_isParent_Active = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Update() {
	    
        if(m_click_Me && m_player_is_Range && !m_isParent_Active)
        {
            //this.transform.
          
            this.transform.parent = GameObject.Find("hairband").transform;
              this.transform.localPosition = Vector3.zero;
   //         this.transform.position = Vector3.zero;
            this.transform.localRotation = Quaternion.identity;
            m_isParent_Active = true;
        }
	}

    void OnMouseDown()
    {
        m_click_Me = true;
    }

    void    OnMouseUp()
    {
        m_click_Me = false;
    }

    void OnTriggerEnter(Collider col_object)
    {

    }

    void OnTriggerStay(Collider col_object)
    {
        //とりあえずプレイヤーの判定用
        string name = col_object.gameObject.name;
        BoxCollider col_type = col_object as BoxCollider;
        if (name == "Player" && col_type)
        {
            m_player_is_Range = true;
        }
    }

    void OnTriggerExit(Collider col_object)
    {
        //とりあえずプレイヤーの判定用
        string name = col_object.transform.ToString();
        BoxCollider col_type = col_object as BoxCollider;
        if (name =="Player"  && col_type)
        {
            m_player_is_Range = false;
        }
    }

}
