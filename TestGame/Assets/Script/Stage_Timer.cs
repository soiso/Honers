using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class Stage_Timer : MonoBehaviour {

    //[SerializeField]
    private float stage_rimit;

    private float start_time=0.0f;
    private float current_time;
    
    private bool s_flag;

    private NumberRenderer rimit_time;
    [SerializeField]
    private GameObject endcount;

    public Texture[] count;
    public AudioClip clip;
    public AudioClip start;
    public AudioClip end;
    private AudioSource audio;
    // Use this for initialization
	void Start () {
        s_flag = false;
        
        rimit_time = GetComponentInChildren<NumberRenderer>();
        audio = GetComponent<AudioSource>();
        audio.clip = clip;
	}
    public void Time_Start()
    {
        start_time = Time.time;
        rimit_time.enabled = true;

    }
    public void StartSEPlay()
    {
        audio.Stop();
        audio.clip = start;
        audio.Play();
    }
    public void Reset()
    {
        start_time = Time.time;
        s_flag = false;
        audio.Stop();
    }
	
	// Update is called once per frame
	void Update () {
            if (Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() != "New_Stage1" &&
                Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() != "New_Stage2" &&
                Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() != "New_Stage3" &&
                Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() != "New_Stage4" &&
                Objectmanager.m_instance.m_scene_manager.GetCurrentStageName() != "New_Stage5")
                return;
        int remaining_time = (int)stage_rimit - ((int)current_time - (int)start_time);
        current_time = Time.time;
        Color temp = endcount.GetComponent<Renderer>().material.color;
        if (remaining_time < 6.0f && remaining_time > 1.0f)
        {
            endcount.GetComponent<Renderer>().material.color = new Color(temp.r, temp.g,temp.b, 120);
            endcount.GetComponent<Renderer>().material.mainTexture = count[(int)remaining_time-1];
            audio.clip = clip;
            if(!audio.isPlaying)audio.Play();
        }
        if(remaining_time <= 1.0f && s_flag != true)
        {
           
            endcount.GetComponent<Renderer>().material.color = new Color(temp.r, temp.g, temp.b, 120);
            endcount.GetComponent<Renderer>().material.mainTexture = count[5];
            this.transform.position = new Vector3(0, 11.28f, -3.5f);
            this.transform.localScale = new Vector3(3,1,1);
        }
        if (start_time < 0.1f) current_time = 0.0f;
        if (current_time - start_time > stage_rimit && 
        s_flag != true)
        {
            ChangeResult();
            s_flag = true;
            rimit_time.enabled = false;
            audio.Stop();
            audio.clip = end;
            audio.Play();
	    }
        rimit_time.SetNumber(remaining_time,false);
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
