using UnityEngine;
using System.Collections;

public class Staer : MonoBehaviour {

    private BoxCollider m_leftcollider;
    private BoxCollider m_rightcollider;

	void Start () 
    {
        m_leftcollider = this.transform.GetChild(0).GetComponent<BoxCollider>();
        m_rightcollider = this.transform.GetChild(1).GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void ElectricLight_ON()
    {
        m_leftcollider.enabled = false;
        m_rightcollider.enabled = false;

        var material = GetComponent<MeshRenderer>().material;
        material.color = Color.white;
    }

    public  void ElectricLight_OFF()
    {
        m_leftcollider.enabled = true;
        m_rightcollider.enabled = true;

        var material = GetComponent<MeshRenderer>().material;
        material.color = Color.black;
    }
}
