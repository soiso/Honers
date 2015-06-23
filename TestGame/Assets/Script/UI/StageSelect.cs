using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour
{
    [SerializeField, SceneName]
    public string sceneName;
    private GameObject manager;

    
    void Start()
    {
        manager = GameObject.Find("SceneManager");
    }
    public void On_Click()
    {
        manager.GetComponent<SceneManager>().NextSceneLoad(sceneName);
    }
    void Update()
    {
        //if (LoadFlg) return;

        //if(Objectmanager.m_instance.m_scene_manager.LoadProgress())
        //{
        //    Objectmanager.m_instance.m_scene_manager.BeginLoad();
        //    LoadFlg = true;
        //}
    }

}
