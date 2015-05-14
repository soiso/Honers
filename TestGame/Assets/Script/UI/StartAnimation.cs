using UnityEngine;
using System.Collections;

public class StartAnimation : MonoBehaviour {

    public bool IsFinish = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnAnimationFinish()
    {
        this.IsFinish = true;
    }
}
