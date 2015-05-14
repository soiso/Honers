#if UNITY_EDITOR
using UnityEngine;
using System.Collections;

public class Stage_Select_Canvas : MonoBehaviour {

    void Start()
    {
        GetComponent<Canvas>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
#endif