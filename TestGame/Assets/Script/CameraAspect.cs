﻿using UnityEngine;
using System.Collections;

public class CameraAspect : MonoBehaviour {


    private Camera m_target_Camera = null;
    [SerializeField]
    private float m_target_width = 640.0f;
    [SerializeField]
    private float m_target_Height = 960.0f;
    
    Rect    m_rect;

	// Use this for initialization
	void Start () {
        m_rect = Camera.main.rect;
        m_target_Camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {

        float target_Aspect = m_target_width / m_target_Height;
        float current_Aspect = Screen.width / Screen.height;


        float ratio = current_Aspect / target_Aspect;

        if (1.0f > ratio)
        {
            m_rect.x = 0f;
            m_rect.width = 1.0f;
            m_rect.y = (1.0f - ratio) / 2.0f;
            m_rect.height = ratio;
           // m_target_Camera.orthographicSize = Screen.width / 2.0f;
        }
        else
        {
            ratio = 1.0f / ratio;
            m_rect.x = (1.0f - ratio) / 2.0f;
            m_rect.width = ratio;
            m_rect.y = .0f;
            m_rect.height = 1.0f;
          //  m_target_Camera.orthographicSize = Screen.height / 2.0f;
        }
        Camera.main.rect = m_rect;
	}
    
}
