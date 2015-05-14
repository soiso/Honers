using UnityEngine;
using System.Collections;

public class NewGame_Button : MonoBehaviour {
    private GameObject manager;
	// Use this for initialization
	void Start () {
        manager = GameObject.Find("SceneManager");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void On_Click()
    {
        manager.GetComponent<SceneManager>().NextSceneLoad();
    }
}
