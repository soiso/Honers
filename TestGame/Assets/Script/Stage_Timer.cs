using UnityEngine;
using System.Collections;

public class Stage_Timer : MonoBehaviour {

    [SerializeField]
    public float stage_rimit;
    private float start_time;
    private float current_time;

    private bool s_flag;
	// Use this for initialization
	void Start () {
        start_time = Time.time;
        s_flag = false;
	}
    public void Reset()
    {
        start_time = Time.time;
        s_flag = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() == "TitleTest") return;
        current_time = Time.time;
        if(current_time - start_time >stage_rimit && s_flag != true){
            ChangeResult();
            s_flag = true;
	    }
        
    }

    public void ChangeResult()
    {
            Objectmanager.m_instance.m_scene_manager.EndStage();
    }
}
