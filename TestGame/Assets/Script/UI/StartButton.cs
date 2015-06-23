
using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private GameObject stage_select;
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject TitleLogo;

    private bool OpenFlg=false;
    void Start()
    {
        stage_select.GetComponent<Canvas>().enabled = false;
    }

    void Update()
    {
        if (OpenFlg) return;
        if(IsTouch())
        {
            OpenDoor();
        }
    }

    private bool IsTouch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            return true;

        return false;
    }
    private void OpenDoor()
    {
            stage_select.GetComponent<Canvas>().enabled = true;
            door.GetComponent<OpenDoor>().Begin_Rotate();
            OpenFlg = true;
    }

}
