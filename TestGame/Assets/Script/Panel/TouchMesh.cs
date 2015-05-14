using UnityEngine;
using System.Collections;

public class TouchMesh : MonoBehaviour {

    public bool m_is_select { get; set; }

	void Start () {
        m_is_select = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

#if UNITY_STANDALONE
    void OnMouseDown()
    {
        var changer = this.transform.parent.parent.GetComponent<PanelChanger>();
        //ひどいからそのうちなおす
        if (changer.m_current_Selectpanel == 2)
        {
            if (m_is_select)
                changer.SubCount_SelectPanel();
            m_is_select = false;
            return;
        }

        if (changer.m_current_Selectpanel < 2)
        {
            m_is_select = (m_is_select) ? false : true;

            if (m_is_select)
                changer.AddCount_SelectPanel();
            else
                changer.SubCount_SelectPanel();
        }
    }

#elif UNITY_ANDROID || UNITY_IOS

    private Vector3 m_TouchStartPos;
    private Vector3 m_TouchEndPos;

    public void IsTouch()
    {
        if( Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (gameObject.GetComponent<BoxCollider>().Raycast(ray, out hit, 50.0f) == false) return;

            m_TouchStartPos = Input.mousePosition;

            var changer = this.transform.parent.parent.GetComponent<PanelChanger>();
            //ひどいからそのうちなおす
            if (changer.m_current_Selectpanel == 2)
            {
                if (m_is_select)
                    changer.SubCount_SelectPanel();
                m_is_select = false;
                return;
            }

            if (changer.m_current_Selectpanel < 2)
            {
                m_is_select = (m_is_select) ? false : true;

                if (m_is_select)
                    changer.AddCount_SelectPanel();
                else
                    changer.SubCount_SelectPanel();
            }
        }
        if( Input.GetKeyUp(KeyCode.Mouse0))
        {

            m_TouchEndPos = Input.mousePosition;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (gameObject.GetComponent<BoxCollider>().Raycast(ray, out hit, 50.0f) == false) return;

            var changer = this.transform.parent.parent.GetComponent<PanelChanger>();
            //ひどいからそのうちなおす
            if (changer.m_current_Selectpanel == 2)
            {
                if (m_is_select)
                    changer.SubCount_SelectPanel();
                m_is_select = false;
                return;
            }

            if (changer.m_current_Selectpanel < 2)
            {
                m_is_select = (m_is_select) ? false : true;

                if (m_is_select)
                    changer.AddCount_SelectPanel();
                else
                    changer.SubCount_SelectPanel();
            }
        }
    }

#endif
}
