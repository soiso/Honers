using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour 
{

    [SerializeField, HeaderAttribute("タイトルBGM")]
    public AudioClip m_TitleBGM;
    [SerializeField, HeaderAttribute("ステージ1BGM")]
    public AudioClip m_Stage1BGM;
    [SerializeField, HeaderAttribute("ステージ2BGM")]
    public AudioClip m_Stage2BGM;
    [SerializeField, HeaderAttribute("ステージ3BGM")]
    public AudioClip m_Stage3BGM;
    [SerializeField, HeaderAttribute("ステージ4BGM")]
    public AudioClip m_Stage4BGM;
    [SerializeField, HeaderAttribute("リザルトBGM")]
    public AudioClip m_ResultBGM;
    private AudioSource m_Source { get; set; }

	// Use this for initialization
	void Start () {
        m_Source = GetComponent<AudioSource>();
        m_Source.clip = m_TitleBGM;
        m_Source.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play()
    {
        m_Source.Play();
    }

    public void Stop()
    {
        m_Source.Stop();
    }

    //BGM遷移やっつけ
    public void ChangeBGM( string StageName )
    {
        m_Source.Stop();

        //タイトル
        if( StageName == "TitleTest") m_Source.clip = m_TitleBGM;
        //ステージ1
        else if (StageName == "New_Stage1") m_Source.clip = m_Stage1BGM;
        //ステージ2
        else if (StageName == "New_Stage2") m_Source.clip = m_Stage2BGM;
        //ステージ3
        else if (StageName == "New_Stage3") m_Source.clip = m_Stage3BGM;
        //ステージ4
        else if (StageName == "New_Stage4") m_Source.clip = m_Stage4BGM;
        //リザルト
        else if (StageName == "Result") m_Source.clip = m_ResultBGM;

        m_Source.Play();
    }

    public void ChangeBGM( AudioClip sound )
    {
        m_Source.Stop();
        m_Source.clip = sound;
        m_Source.Play();
    }
}
