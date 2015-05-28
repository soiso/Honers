using UnityEngine;
using System.Collections;

public class himawaritrigger : MonoBehaviour {

    bool m_isActive = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter(Collider other)
    {
        string layer_name = LayerMask.LayerToName(other.gameObject.layer);
        if (layer_name == "Player")
        {
            other.gameObject.transform.parent = this.transform;
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
        }
    }





}
