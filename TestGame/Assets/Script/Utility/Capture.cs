using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;
using System;

public class Capture : MonoBehaviour {

    [SerializeField]
    private Camera m_target_Camera;

    public Texture2D Texture = null;

    /// <summary>
    /// キャプチャー画像を保存済みかどうか
    /// </summary>
    bool saved_screen_capture = false;

    /// <summary>
    /// Awake this instance.
    /// </summary>
    void Awake()
    {
        saved_screen_capture = false;
    }

    /// <summary>
    /// Raises the post render event.
    /// </summary>
    void Update()
    {
        if (saved_screen_capture != true)
        {
            // キャプチャー
            Take();

            GameObject t = GameObject.Find("Test");
            t.GetComponent<MeshRenderer>().material.mainTexture = this.Texture;
            // 破棄
            // Destroy(this);
        }
    }

    private void Take()
    {
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture rt = new RenderTexture(screenShot.width, screenShot.height, 24);


        RenderTexture pre = m_target_Camera.targetTexture;
        m_target_Camera.targetTexture = rt;
        m_target_Camera.Render();
        m_target_Camera.targetTexture = pre;

        RenderTexture.active = rt;
        screenShot.ReadPixels(new Rect(0, 0, screenShot.width, screenShot.height), 0, 0);

        //various other post processing here..

        screenShot.Apply();

        this.Texture = screenShot;
        saved_screen_capture = true;
    }
}
