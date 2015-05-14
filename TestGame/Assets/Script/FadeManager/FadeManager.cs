#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// シーン遷移時のフェードイン・アウトを制御するためのクラス .
/// </summary>
public class FadeManager : MonoBehaviour
{

    //#region Singleton

    //private static FadeManager instance;

    //public static FadeManager Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = (FadeManager)FindObjectOfType(typeof(FadeManager));

    //            if (instance == null)
    //            {
    //                Debug.LogError(typeof(FadeManager) + "is nothing");
    //            }
    //        }

    //        return instance;
    //    }
    //}

    //#endregion Singleton

    /// <summary>
    /// デバッグモード .
    /// </summary>
    public bool DebugMode = true;
    /// <summary>フェード中の透明度</summary>
    protected float fadeAlpha = 0;
    /// <summary>フェード中かどうか</summary>
    protected bool isFading = false;
    /// <summary>フェード色</summary>
    public Color fadeColor = Color.black;

    protected AsyncOperation loadInfo;

    public void Awake()
    {
        //if (this != Instance)
        //{
        //    Destroy(this.gameObject);
        //    return;
        //}
        DontDestroyOnLoad(this.gameObject);
    }

    //public void OnGUI()
    //{

    //    // Fade .
    //    if (this.isFading)
    //    {
    //        //色と透明度を更新して白テクスチャを描画 .
    //        this.fadeColor.a = this.fadeAlpha;
    //        GUI.color = this.fadeColor;
    //        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);
    //    }

    //    if (this.DebugMode)
    //    {
    //        if (!this.isFading)
    //        {
    //            //Scene一覧を作成 .
    //            //(UnityEditor名前空間を使わないと自動取得できなかったので決めうちで作成) .
    //List<string> scenes = new List<string>();
    //            scenes.Add("testGame");
    //            scenes.Add("MenuScene");
    //            //scenes.Add ("SomeScene2");


    //            //Sceneが一つもない .
    //            if (scenes.Count == 0)
    //            {
    //                GUI.Box(new Rect(10, 10, 200, 50), "Fade Manager(Debug Mode)");
    //                GUI.Label(new Rect(20, 35, 180, 20), "Scene not found.");
    //                return;
    //            }


    //            GUI.Box(new Rect(10, 10, 300, 50 + scenes.Count * 25), "Fade Manager(Debug Mode)");
    //            GUI.Label(new Rect(20, 30, 280, 20), "Current Scene : " + Application.loadedLevelName);

    //            int i = 0;
    //            foreach (string sceneName in scenes)
    //            {
    //                if (GUI.Button(new Rect(20, 55 + i * 25, 100, 20), "Load Level"))
    //                {
    //                    LoadLevel(sceneName, 1.0f);
    //                }
    //                GUI.Label(new Rect(125, 55 + i * 25, 1000, 20), sceneName);
    //                i++;
    //            }
    //        }
    //    }



    //}

    /// <summary>
    /// 画面遷移 .
    /// </summary>
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    public void LoadLevel(string scene, float interval)
    {
        StartCoroutine(TransScene(scene, interval));
    }

    public void LoadLevel_Add(string scene, float interval)
    {
        StartCoroutine(TransScene_Add(scene, interval));
    }
    
   public void LoadLevelAssrecc(string new_scene)
   {
       StartCoroutine(TransScene(new_scene,1));

   }
    /// シーン遷移用コルーチン .
    /// <param name='scene'>シーン名</param>
    /// <param name='interval'>暗転にかかる時間(秒)</param>
    private IEnumerator TransScene(string scene, float interval)
    {
        //だんだん暗く .
        this.isFading = true;
        float time = 0;
        
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        //シーン切替 .

        loadInfo = Application.LoadLevelAsync(scene);
        //だんだん明るく .
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }

    private IEnumerator TransScene_Add(string scene, float interval)
    {
        //だんだん暗く .
        this.isFading = true;
        float time = 0;

        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(0f, 1f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        //シーン切替 .
        Application.LoadLevel("MasterScene");
        foreach (var guid in AssetDatabase.FindAssets(scene))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            string scenename = System.IO.Path.GetFileNameWithoutExtension(path);
            string extension = System.IO.Path.GetExtension(path);
            if(extension == ".unity")
            Application.LoadLevelAdditive(scenename);
        }
        //だんだん明るく .
        time = 0;
        while (time <= interval)
        {
            this.fadeAlpha = Mathf.Lerp(1f, 0f, time / interval);
            time += Time.deltaTime;
            yield return 0;
        }

        this.isFading = false;
    }
}
#endif