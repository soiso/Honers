
using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour
{
    private GameObject title_menu;
    private GameObject game_menu;

    void Start()
    {
        title_menu = GameObject.Find("Title");
        game_menu = GameObject.Find("Stage_Select");
    }

    void Update()
    {
        //this.gameObject.transform.rotation = 
        //    gameObject.transform.parent.transform.rotation;
    }
    public void On_Click()
    {
        title_menu.GetComponent<Canvas>().enabled = false;
        game_menu.GetComponent<Canvas>().enabled = true;
    }


}
