using UnityEngine;
using System.Collections;

public class Story : MonoBehaviour {

    [SerializeField, SceneName]
    private string nect_scene_name;
    [SerializeField, Range(0, 9997), HeaderAttribute("BとAの境")]
    public int score_sort_AB;
    [SerializeField, Range(0, 9999), HeaderAttribute("AとSの境")]
    public int score_sort_SA;

    private int Score = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Touch())
        {
            NextScene();
        }
	}
    public void NextScene()
    {
        Objectmanager.m_instance.m_fruit_Counter.Reset();
        if(Objectmanager.m_instance.m_scene_manager.currentSceneName == "story_11")
            Objectmanager.m_instance.m_scene_manager.NextSceneLoad(CheckEnding());
        else
            Objectmanager.m_instance.m_scene_manager.NextSceneLoad(nect_scene_name);
    }

    private Vector3 m_StartPos;
    private Vector3 m_EndPos;
    private bool Touch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_StartPos = Input.mousePosition;
            m_StartPos.z = .0f;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            m_EndPos = Input.mousePosition;
            m_EndPos.z = .0f;
            Vector3 FlickVec = m_EndPos - m_StartPos;
            FlickVec.Normalize();
            Vector3 Horlizon = new Vector3(1.0f, .0f, .0f);
            if (Vector3.Dot(FlickVec, Horlizon) < .0f)
            {
                return true;
            }
        }

        return false;
    }
    private string CheckEnding()
    {

        switch(Calculate()){
            case 1:
        return "story_bad";
            case 2:
        return "story_normal";
            case 3:
        return "story_happy";
        }
        return null;
    }
    int Calculate()
    {
        if (Score < score_sort_AB)
            return 1;
        if (Score > score_sort_AB && Score < score_sort_SA)
            return 2;
        if (Score > score_sort_SA)
            return 3;
        return 0;
    }
}
