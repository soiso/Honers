using UnityEngine;
using System.Collections;

public class StageSelect : MonoBehaviour
{
    [SerializeField, SceneName]
    public string sceneName;
    private GameObject manager;

    private bool LoadFlg = false;
    void Start()
    {
        manager = GameObject.Find("SceneManager");
    }
    public void On_Click()
    {
        if (LoadFlg) return;
        manager.GetComponent<SceneManager>().NextSceneLoad(sceneName);
        Objectmanager.m_instance.m_scene_manager.BeginLoad();
        LoadFlg = true;
    }
    void Update()
    {

    }

}
