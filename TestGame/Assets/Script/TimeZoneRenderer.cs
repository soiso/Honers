using UnityEngine;
using System.Collections;

public class TimeZoneRenderer : MonoBehaviour {

    [SerializeField]
    private Sprite[] m_spritelist;

    private int m_current_Index;

    private SpriteRenderer m_SpriteRenderer;

    void Awake()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
    }

	void Start () 
    {
      
    }
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void SetRenderTime(int set_time)
    {
        m_current_Index = set_time;
        m_SpriteRenderer.sprite = m_spritelist[m_current_Index];

    }
}
