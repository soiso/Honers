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
        if(sceneName == "TitleTest")
        {
            manager.GetComponent<SceneManager>().ChangeScene(sceneName);
        }
        else
        {
            manager.GetComponent<SceneManager>().ChangeScene_Add(sceneName);
        }


    }

}
