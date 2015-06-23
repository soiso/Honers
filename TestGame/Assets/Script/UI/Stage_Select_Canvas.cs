#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class Stage_Select_Canvas : MonoBehaviour {
    private bool LoadFlg = false;
    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (LoadFlg) return;

        if (Objectmanager.m_instance.m_scene_manager.LoadProgress())
        {
            Objectmanager.m_instance.m_scene_manager.BeginLoad();
            LoadFlg = true;
        }
    }
}
#endif