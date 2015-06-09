using UnityEngine;
using System.Collections;

public class himawaritrigger : MonoBehaviour {

    bool m_isActive = false;

    GameObject m_player = null;
    Vector3 m_player_LossyScale;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if(m_player)
        {
            ScaleAdjust();
        }
	}

    void ScaleAdjust()
    {
        Vector3 scale;
        scale.x = m_player.transform.localScale.x / m_player.transform.lossyScale.x * m_player_LossyScale.x;
        scale.y = m_player.transform.localScale.y / m_player.transform.lossyScale.y * m_player_LossyScale.y;
        scale.z = m_player.transform.localScale.z / m_player.transform.lossyScale.z * m_player_LossyScale.z;

        m_player.transform.localScale = scale;
  
    }

    public void OnTriggerEnter(Collider other)
    {
        string layer_name = LayerMask.LayerToName(other.gameObject.layer);
        if (layer_name == "Player")
        {
            other.gameObject.transform.parent = this.transform;
            m_player = other.gameObject;
            m_player_LossyScale = m_player.transform.lossyScale;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        string layer_name = LayerMask.LayerToName(other.gameObject.layer);
        if (layer_name == "Player")
        {
            other.gameObject.transform.parent = this.transform.root;
            other.gameObject.transform.localScale = new Vector3(1, 1, 1);
            Vector3 pos = other.gameObject.transform.position;
            pos.z = 0f;
            other.gameObject.transform.position = pos;
            m_player.transform.localScale = Vector3.one;
            m_player = null;
        }
    }

}
