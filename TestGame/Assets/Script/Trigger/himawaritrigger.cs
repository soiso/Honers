using UnityEngine;
using System.Collections;

public class himawaritrigger : MonoBehaviour {

  

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
            other.gameObject.transform.parent = null;
        }
    }





}
