using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class Capture : MonoBehaviour {

    void Update()
    {
      //  Capture_Camera(Objectmanager.m_instance.m_screenShot_Camera);
    }

    public Texture2D Capture_Camera(Camera target_Camera)
    {
        Texture2D ret = new Texture2D(target_Camera.pixelWidth, target_Camera.pixelHeight, 
            TextureFormat.RGB24, false);
        int a = Screen.width;
        int b = Screen.height;

        RenderTexture work_Buffer =
            new RenderTexture(Screen.width, Screen.height, 24);

        RenderTexture pre = target_Camera.targetTexture;
        target_Camera.targetTexture = work_Buffer;
        target_Camera.Render();
        target_Camera.targetTexture = pre;
        
       RenderTexture.active = work_Buffer;
        //これがtargetTexture->テクスチャへの焼きこみ関数
       ret.ReadPixels(new Rect((int)(target_Camera.rect.xMin * ret.width), 0,
                                              target_Camera.pixelWidth,target_Camera.pixelHeight),
                                                    0, 0);

        ret.Apply();
        return ret;
    }
}
