
using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour
{
    [SerializeField]
    private GameObject stage_select;
    [SerializeField]
    private GameObject door;

    void Start()
    {
        
    }

    void Update()
    {
        if(IsTouch())
        {
            stage_select.GetComponent<Canvas>().enabled = true;
            door.GetComponent<OpenDoor>().Begin_Rotate();
        }
    }

    private bool IsTouch()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            return true;

        return false;
    }


}
