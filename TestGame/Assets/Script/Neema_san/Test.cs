using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

    private Camera _mainView = null;
    private Camera mainView
    {
        get { if (!_mainView) _mainView = Camera.main; return _mainView; }
        set { _mainView = value; }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {

    }

    void LateUpdate()
    {

    }
}
