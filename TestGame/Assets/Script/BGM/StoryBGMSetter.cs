using UnityEngine;
using System.Collections;

public class StoryBGMSetter : MonoBehaviour
{

    [SerializeField, HeaderAttribute("セットするBGM")]
    private AudioClip[] m_SceneBGM;

    [SerializeField, HeaderAttribute("再生するタイミング")]
    private GameObject[] m_Quad;

    void Update()
    {

    }

    public void SetBGM( int index )
    {
        Objectmanager.m_instance.m_BGM.ChangeBGM(m_SceneBGM[index], true);
    }
}
