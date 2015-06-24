using UnityEngine;
using System.Collections;

public class SetNextScene : MonoBehaviour {

    [SerializeField, SceneName]
    private string next_scene;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public string GetNextSceneName()
    {
        return next_scene;
    }
}
