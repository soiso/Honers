using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class Stage_Timer : MonoBehaviour {

    //[SerializeField]
    private float stage_rimit;

    private float start_time=0.0f;
    private float current_time;
    
    private bool s_flag;

    private Text rimit_time;
    [SerializeField]
    private GameObject endcount;

    public Texture[] count;
    // Use this for initialization
	void Start () {
        s_flag = false;
        
        rimit_time = GetComponentInChildren<Text>();
	}
    public void Time_Start()
    {
        start_time = Time.time;
        rimit_time.enabled = true;
    }
    public void Reset()
    {
        start_time = Time.time;
        s_flag = false;
    }
	
	// Update is called once per frame
	void Update () {
            if (Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() != "New_Stage1" &&
                Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() != "New_Stage2" &&
                Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() != "New_Stage3" &&
                Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() != "New_Stage4" &&
                Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() != "New_Stage5")
                return;
        float remaining_time = stage_rimit - (current_time - start_time);
        current_time = Time.time;
        if (remaining_time < 5.0f && remaining_time > 0.0f)
        {
            Color temp = endcount.GetComponent<Renderer>().material.color;
            endcount.GetComponent<Renderer>().material.color = new Color(temp.r, temp.g,temp.b, 255);
            endcount.GetComponent<Renderer>().material.mainTexture = count[(int)remaining_time];
        }
        if (start_time < 0.1f) current_time = 0.0f;
        if (current_time - start_time > stage_rimit && 
        s_flag != true)
        {
            ChangeResult();
            s_flag = true;
            rimit_time.enabled = false;
	    }
        rimit_time.text = remaining_time.ToString("f2");
    }

    public void ChangeResult()
    {
        Color temp = endcount.GetComponent<Renderer>().material.color;
        endcount.GetComponent<Renderer>().material.color = new Color(temp.r, temp.g, temp.b, 0);
          Objectmanager.m_instance.m_scene_manager.EndStage();
    }
    public void SetStageRimit(float num)
    {
        stage_rimit = num;
    }
}
