using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour 
{
    private AudioSource m_Source { get; set; }

	// Use this for initialization
	void Start () 
    {
        m_Source = GetComponent<AudioSource>();
	}

    public void Play()
    {
        m_Source.Play();
    }

    public void Stop()
    {
        m_Source.Stop();
    }

    public void ChangeBGM( AudioClip sound )
    {
        m_Source.Stop();
        m_Source.clip = sound;
        m_Source.Play();
    }
}
