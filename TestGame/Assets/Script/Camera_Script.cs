using UnityEngine;
using System.Collections;

public class Camera_Script : MonoBehaviour {

    [SerializeField]
    public Vector2 m_aspect = new Vector2(4,3);
    private float m_aspect_Rate;
    [SerializeField]
    private Camera m_camera;
    [SerializeField]
    Vector2 m_screen_Size = new Vector2(1280, 720);

	void Start () 
    {
        m_aspect_Rate = m_aspect.x / m_aspect.y;
        m_camera = Camera.main;
	}
	
	void    UpdateAspect()
    {
        float base_aspect = m_aspect.y  / m_aspect.x;
        float current_aspect = (float)Screen.height / (float)Screen.width;
        
    }

	void Update () 
    {
        
        m_camera.rect = new Rect(0f, 0f, 0.95f, 1f);
        m_camera.aspect = m_aspect_Rate;
 
	}
}
