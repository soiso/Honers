using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {




    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            var renderer = GetComponent<MeshRenderer>();
            renderer.material.mainTexture = Objectmanager.m_instance.m_scshot_Machine.Capture_Camera(Objectmanager.m_instance.m_screenShot_Camera);

        }
    }
}
