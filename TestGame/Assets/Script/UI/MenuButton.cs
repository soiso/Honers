using UnityEngine;
using System.Collections;

public class MenuButton : SceneManager
{
    private GameObject game_menu;

    void Start()
    {
        game_menu = GameObject.Find("GameMenu");
    }
    public void On_Click()
    {
        //manager.GetComponent<SceneManager>().OpenMenu();
        //game_menu.GetComponent<Canvas>().enabled = true;
        foreach (string name in sceneName)
        {
            ChangeScene_Add(name);
        }
    }

    void OnGUI()
    {
        
    }
}
