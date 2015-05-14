using UnityEngine;
using System.Collections;

public class PanelParametor : MonoBehaviour {
    
     public enum  TIMEZONE
     {
         error = -1,
         morning,
         noon,
         night,
     }

     [SerializeField]
     private TIMEZONE m_timeZone;
     public TIMEZONE GetTimezone { get { return m_timeZone; } }
   
     public bool m_isChangeNow { get; private set; }
     public bool m_collider_isActive { get; private set; }
     [SerializeField]
     private Color select_Color = Color.red;

     public TouchMesh m_touchMesh { get; private set; }

    // public float m_leap_Speed;

    void Start()
     {
         m_touchMesh = this.transform.GetChild(0).GetComponent<TouchMesh>();
     }

    public void Change_Begin()
    {
        m_isChangeNow = true;
        m_collider_isActive = false;
        m_touchMesh.m_is_select= false;
    }

    public void Change_End()
    {
        m_isChangeNow = false;
        m_collider_isActive = true;
    }

    void   Calculate_AlbedoColor()
    {
        if (m_touchMesh.m_is_select)
        {
            var material = GetComponent<MeshRenderer>();
            material.material.color = select_Color;
        }
        else
        {
            var material = GetComponent<MeshRenderer>();
            material.material.color = Color.white;
        }
    }

    void Update()
     {
#if UNITY_ANDROID || UNITY_IOS
        m_touchMesh.IsTouch();
#endif
         Calculate_AlbedoColor();
     }

    public void Change_Timezone(TIMEZONE change_Zone)
    {
        m_timeZone = change_Zone;
    }

//#if UNITY_STANDALONE
//    void OnMouseDown()
//    {
//        var changer = this.transform.root.GetComponent<PanelChanger>();
//        //ひどいからそのうちなおす
//        if (changer.m_current_Selectpanel == 2)
//        {
//            if (m_is_select)
//                changer.SubCount_SelectPanel();
//            m_is_select = false;
//            return;
//        }

//        if (changer.m_current_Selectpanel < 2)
//        {
//           m_is_select = (m_is_select) ? false : true;

//        if(m_is_select)
//            changer.AddCount_SelectPanel();
//        else
//            changer.SubCount_SelectPanel();         
//        }
//    }

//#elif UNITY_ANDROID || UNITY_IOS

//    private Vector3 m_TouchStartPos;
//    private Vector3 m_TouchEndPos;

//    void IsTouch()
//    {
//        if( Input.GetKeyDown(KeyCode.Mouse0))
//        {
//            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;
//            if (gameObject.GetComponent<MeshCollider>().Raycast( ray, out hit, 50.0f ) == false) return;

//            m_TouchStartPos = Input.mousePosition;

//            var changer = this.transform.root.GetComponent<PanelChanger>();
//            //ひどいからそのうちなおす
//            if (changer.m_current_Selectpanel == 2)
//            {
//                if (m_is_select)
//                    changer.SubCount_SelectPanel();
//                m_is_select = false;
//                return;
//            }

//            if (changer.m_current_Selectpanel < 2)
//            {
//                m_is_select = (m_is_select) ? false : true;

//                if (m_is_select)
//                    changer.AddCount_SelectPanel();
//                else
//                    changer.SubCount_SelectPanel();
//            }
//        }
//        if( Input.GetKeyUp(KeyCode.Mouse0))
//        {

//            m_TouchEndPos = Input.mousePosition;

//            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//            RaycastHit hit;
//            if (gameObject.GetComponent<MeshCollider>().Raycast( ray, out hit, 50.0f ) == false) return;

//            var changer = this.transform.root.GetComponent<PanelChanger>();
//            //ひどいからそのうちなおす
//            if (changer.m_current_Selectpanel == 2)
//            {
//                if (m_is_select)
//                    changer.SubCount_SelectPanel();
//                m_is_select = false;
//                return;
//            }

//            if (changer.m_current_Selectpanel < 2)
//            {
//                m_is_select = (m_is_select) ? false : true;

//                if (m_is_select)
//                    changer.AddCount_SelectPanel();
//                else
//                    changer.SubCount_SelectPanel();
//            }
//        }
//    }

//#endif

}
