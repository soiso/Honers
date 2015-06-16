using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResultBackGround : MonoBehaviour {

	// Use this for initialization
    void Awake()
    {
        this.GetComponent<Renderer>().material.mainTexture = Objectmanager.m_instance.m_scshot_Machine.Capture_Camera(Objectmanager.m_instance.m_screenShot_Camera);
        
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
