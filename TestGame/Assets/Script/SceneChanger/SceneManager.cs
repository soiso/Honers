
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using System.Linq;



public class SceneManager : MonoBehaviour
{
    [SerializeField, SceneName]
    public string[] sceneName;
    [SerializeField]
    private float[] rimit_time;
    [SerializeField]
    private GameObject light;
    [SerializeField]
    private GameObject fever_sign;
    [SerializeField]
    public int[] stage_pic_num;

    private GameObject picpaper;
    private GameObject startsign;
    private AsyncOperation loadInfo;
    public int currentScene_num = 0;
    public int LoadStage_num = 0;
    public string currentSceneName;

    private GameObject oldPicPaper;
    private bool LoadFlg = false;   //ロード中かどうか
    private bool ChangeFlg = false;

    private bool ResultFlg = false;

    // Use this for initialization
    void Start()
    {
        currentSceneName = "TitleTest";
        startsign = GameObject.Find("StartSign");
        oldPicPaper = null;
        Objectmanager.m_instance.m_camera_move.Init();
    }

    public void Init()
    {
        LoadFlg = false;   //ロード中かどうか
        loadInfo = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (ResultFlg)
        {
            light.GetComponent<Light>().intensity = Mathf.Lerp(light.GetComponent<Light>().intensity, 0.0f, 0.03f);
            float inset = light.GetComponent<Light>().intensity;
 
        }

        if (oldPicPaper != null)
        {
            if (oldPicPaper.GetComponent<PicturePaper>().GetTargetRange() < 20.0f)
            {
                if (currentSceneName == "New_Stage1" ||
                    currentSceneName == "New_Stage2" ||
                    currentSceneName == "New_Stage3" ||
                    currentSceneName == "New_Stage4" ||
                    currentSceneName == "New_Stage5")
                {
                    Objectmanager.m_instance.m_camera_move.cMove_Begin();
                    startsign.GetComponent<StartSign>().AlphaIncrease_Begin();
                    Objectmanager.m_instance.m_stage_timer.StartSEPlay();
                }
                else Time.timeScale = 1;
                GameObject.Destroy(oldPicPaper);
                //LoadFlg = false;
                ChangeFlg = false;
            }
        }

        if (startsign.GetComponent<StartSign>().AlphaIncrease())
        {
            Time.timeScale = 1;
            Objectmanager.m_instance.m_camera_move.cMove_Begin();
            Objectmanager.m_instance.m_stage_timer.Time_Start();
        }
    }
    public void BeginLoad()
    {
        if (!LoadFlg) return;
        if (loadInfo.allowSceneActivation) return;
        //シーン切替 .
        if (currentSceneName != "Result" )
            oldPicPaper.GetComponent<PicturePaper>().Move_Begin();
        //else
        LoadFlg = false;
        ResultFlg = false;
        loadInfo.allowSceneActivation = true;
    }
    public bool LoadProgress()
    {
        if (!LoadFlg) return false;
        if (loadInfo.allowSceneActivation) return false;
        //if(loadInfo.progress<0.9f) return false;
        
        return true;
    }
    public void ChangeScene(string sceneName)
    {
        currentSceneName = sceneName;
        Application.LoadLevel(sceneName);
        if (sceneName == "TitleTest")
            currentScene_num = 0;
    }
    public void ChangeScene_Add(string sceneName)
    {
        fever_sign.GetComponent<FeaverSign>().Reset();

        currentSceneName = sceneName;
        if(currentSceneName != "Result")
        Time.timeScale = 0;
        picpaper = GameObject.Find("PicturePaper");
        Objectmanager.m_instance.m_stage_timer.SetStageRimit(rimit_time[currentScene_num]);

        loadInfo = Application.LoadLevelAdditiveAsync(sceneName);
        LoadFlg = true;
        loadInfo.allowSceneActivation = false;
        picpaper.GetComponent<PicturePaper>().SoundPlay();
        picpaper.name += "_old";
        oldPicPaper = picpaper;
        if (currentSceneName == "New_Stage1" ||
            currentSceneName == "New_Stage2" ||
            currentSceneName == "New_Stage3" ||
            currentSceneName == "New_Stage4" ||
            currentSceneName == "New_Stage5")
        {
            Objectmanager.m_instance.m_camera_move.Init();
        }
    }
    public void AddSceneResult()
    {
        loadInfo = Application.LoadLevelAdditiveAsync("Result");
        LoadFlg = true;
        loadInfo.allowSceneActivation = false;
        //ResultFlg = false;
        currentSceneName = "Result";
    }
    public void NextSceneLoad()
    {
        if (LoadFlg) return;
        Objectmanager.m_instance.m_stage_timer.Reset();

        if (currentSceneName != "TitleTest")
        {
            currentSceneName = sceneName[currentScene_num];
            currentScene_num += 1;
            ChangeScene_Add(sceneName[currentScene_num]);
        }
        else
        {
            ChangeScene_Add(sceneName[currentScene_num]);
            return;
        }
        

    }

    public void NextSceneLoad(string newstageName)
    {
        if (LoadFlg) return;
        Objectmanager.m_instance.m_stage_timer.Reset();
        if (newstageName != "Result")
            currentScene_num = 0;
        foreach (string stageName in sceneName)
        {
            if (stageName == newstageName)
            {
                if (newstageName == "TitleTest")
                    ChangeScene(newstageName);
                else
                {
                    ChangeScene_Add(newstageName);
                }
                break;
            }
            if (newstageName != "Result")
                currentScene_num++;
        }
    }

    public void EndStage()
    {
        if (LoadFlg) return;
        ResultFlg = true;
        if (currentSceneName == "New_Stage5")
        {
            LoadStage_num = 5;
            //return;
        }
        AddSceneResult();

    }
    public string GetCurrentStageName()
    {
        return currentSceneName;
    }
}

