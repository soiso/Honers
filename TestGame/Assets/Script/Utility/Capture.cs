using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class Capture : MonoBehaviour {

    //[SerializeField]
    //private Camera m_target_Camera;

    //private Texture2D Texture = null;
    //void Awake()
    //{
    //  //  saved_screen_capture = false;
    //}

    //void Update()
    //{
    //    //if (saved_screen_capture != true)
    //    //{
    //        // キャプチャー
    //       // Take();

    //        //GameObject t = GameObject.Find("Test");
    //        //t.GetComponent<MeshRenderer>().material.mainTexture = this.Texture;
    //        // 破棄
    //        // Destroy(this);
    ////    }
    //}

    public Texture2D Capture_Camera(Camera target_Camera)
    {
        Texture2D ret = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
       
        RenderTexture work_Buffer = new RenderTexture(ret.width, ret.height, 24);

        RenderTexture pre = target_Camera.targetTexture;
        target_Camera.targetTexture = work_Buffer;
        target_Camera.Render();
        target_Camera.targetTexture = pre;

       RenderTexture.active = work_Buffer;
        //これがtargetTexture->テクスチャへの焼きこみ関数
        ret.ReadPixels(new Rect(0, 0, ret.width, ret.height),0,0);

        ret.Apply();
        return ret;
    }
}
