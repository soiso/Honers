using UnityEngine;
using System.Collections;

public class ActiveTimeZoneRenderer : MonoBehaviour
{
    [SerializeField]
    private Sprite[] Grah;

    private SpriteRenderer m_SpriteRenderer;
    PanelParametor.TIMEZONE[] time;

    [SerializeField, Range(1, 5)]
    private int m_RenderTime;

    [SerializeField, Range(1, 5)]
    private int m_ChangeTime;
    private int m_CurrentTime=0;

    private int index = 0;

	// Use this for initialization
	void Start () 
    {
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        m_SpriteRenderer.sprite = Grah[index];
	}
	
	// Update is called once per frame
    void Update()
    {
        var obj = GetComponentInParent<FieldObjectInterface>();
        if (GameObject.Find("Player").GetComponent<Player>().m_StopFrame >= m_RenderTime * 60)
        {
            if (obj.Is_ActiveTimeZone(obj.m_CurrentTimeZone))
            {
                if (obj.m_SleepTimeZone.Length > 1 && m_CurrentTime % (m_ChangeTime * 60) == 0)
                {
                    if (index == 0) index = 1;
                    else index = 0;
                }
                else if( obj.m_SleepTimeZone.Length < 2) index = 0;
                m_CurrentTime++;
                m_SpriteRenderer.sprite = Grah[(int)(obj.m_SleepTimeZone[index])];
            }
            else
            {
                if (obj.m_ActiveTimeZone.Length > 1 && m_CurrentTime % (m_ChangeTime * 60) == 0)
                {
                    if (index == 0) index = 1;
                    else index = 0;
                }
                else if (obj.m_ActiveTimeZone.Length < 2) index = 0;
                m_CurrentTime++;
                m_SpriteRenderer.sprite = Grah[(int)(obj.m_ActiveTimeZone[index])];

            }
            m_SpriteRenderer.enabled = true;
        }
        else
            m_SpriteRenderer.enabled = false;
    }
}
