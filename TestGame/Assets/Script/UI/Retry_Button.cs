#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class Retry_Button : MonoBehaviour
{
    private GameObject game_menu;
    private GameObject manager;

    void Start()
    {
        game_menu = GameObject.Find("GameMenu");
        manager = GameObject.Find("SceneManager");
    }

    public void On_Click()
    {
        manager.GetComponent<SceneManager>().CloseMenu();
    }

    void OnGUI()
    {

    }
}
#endif