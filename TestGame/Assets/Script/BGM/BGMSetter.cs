using UnityEngine;
using System.Collections;

public class BGMSetter : MonoBehaviour
{

    [SerializeField, HeaderAttribute("セットするBGM")]
    private AudioClip m_SceneBGM;

    [SerializeField, HeaderAttribute("BGMをループさせるか")]
    private bool m_Loop = true;

	// Use this for initialization
	void Start () 
    {
        Objectmanager.m_instance.m_BGM.ChangeBGM(m_SceneBGM, m_Loop);
	}
}
