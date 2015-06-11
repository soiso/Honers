using UnityEngine;
using System.Collections;

public class BGMSetter : MonoBehaviour
{

    [SerializeField, HeaderAttribute("セットするBGM")]
    public AudioClip m_SceneBGM;

	// Use this for initialization
	void Start () 
    {
        Objectmanager.m_instance.m_BGM.GetComponent<BGM>().ChangeBGM(m_SceneBGM);
	}
}
