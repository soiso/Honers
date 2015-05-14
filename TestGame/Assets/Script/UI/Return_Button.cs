#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class Return_Button : MonoBehaviour
{
    private GameObject game_menu;
    void Start()
    {
        game_menu = GameObject.Find("GameMenu");
    }

    public void On_Click()
    {
        //CloseMenu();
        //game_menu.GetComponent<Canvas>().enabled = false;
    }
}
#endif