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

    //タッチ開始位置
    public Vector3 m_TouchStartPos;
    //タッチ終了位置
    public Vector3 m_TouchEndPos;

    //選択されたパネルインデックス
    public int m_index { get; private set; }
    public int m_target_index { get; private set; }

    [SerializeField, HeaderAttribute("フリック判定までの遊び")]
    private float m_FlickLen;

    public void IsTouch()
    {
        if( Input.GetKeyDown(KeyCode.Mouse0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (gameObject.GetComponent<BoxCollider>().Raycast(ray, out hit, 50.0f) == false) return;

            GetComponent<BoxCollider>().enabled = false;
            m_TouchStartPos = Input.mousePosition;

            //とりあえず登録されてるパネルと総当りで当たったパネルの検出
            var changer = GetComponentInParent<PanelChanger>();
            for( int i = 0; i < changer.m_Panel.Length; i++ )
            {
                if( changer.m_Panel[i].GetComponent<PanelParametor>().IsTouch( m_TouchStartPos ) )
                {
                    //当たったパネルのインデックス保存
                    m_index = i;
                    break;
                }
            }
            m_TouchStartPos.z = .0f;

            //当たったパネルの操作
            if (changer.m_Panel[m_index].GetComponentInChildren<TouchMesh>().m_is_select)
                changer.m_Panel[m_index].GetComponentInChildren<TouchMesh>().m_is_select = false;
            else
                changer.m_Panel[m_index].GetComponentInChildren<TouchMesh>().m_is_select = true;

            if (changer.m_Panel[m_index].GetComponentInChildren<TouchMesh>().m_is_select)
                changer.AddCount_SelectPanel();
            else
                changer.SubCount_SelectPanel();

            GetComponent<BoxCollider>().enabled = true;

        }
        if( Input.GetKeyUp(KeyCode.Mouse0))
        {

            var changer = GetComponentInParent<PanelChanger>();
            m_TouchEndPos = Input.mousePosition;
            m_TouchEndPos.z = .0f;

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            //外れたら選択解除
            if (gameObject.GetComponent<BoxCollider>().Raycast(ray, out hit, 50.0f) == false)
            {
                if (changer.m_Panel[m_index].GetComponentInChildren<TouchMesh>().m_is_select)
                {
                    changer.SubCount_SelectPanel();
                    changer.m_Panel[m_index].GetComponentInChildren<TouchMesh>().m_is_select = false;
                }
                return;
            }
            GetComponent<BoxCollider>().enabled = false;

            //フリックのベクトル算出
            Vector3 FlickVec = m_TouchStartPos - m_TouchEndPos;
            //遊びの範囲内なら選択解除
            if( Vector3.Distance( m_TouchEndPos, m_TouchStartPos ) < m_FlickLen )
            {
                if (changer.m_Panel[m_index].GetComponentInChildren<TouchMesh>().m_is_select)
                {
                    changer.SubCount_SelectPanel();
                    changer.m_Panel[m_index].GetComponentInChildren<TouchMesh>().m_is_select = false;
                }
                GetComponent<BoxCollider>().enabled = true;
                return;
            }
            FlickVec.Normalize();

            //なす角が１番小さいパネルのインデックス
            float min_Dot = 1.0f;
            for (int i = 0; i < changer.m_Panel.Length; i++ )
            {
                //タッチ開始で選択されたパネルは無視
                if (m_index == i) continue;

                Vector3 v = changer.m_Panel[i].transform.position - changer.m_Panel[m_index].transform.position;
                v.Normalize();
                float dot = Vector3.Dot( FlickVec, v );
                if( dot < min_Dot )
                {
                    min_Dot = dot;
                    m_target_index = i;
                }
            }
            if (min_Dot > -0.5f)
            {
                if (changer.m_Panel[m_index].GetComponentInChildren<TouchMesh>().m_is_select)
                {
                    changer.SubCount_SelectPanel();
                    changer.m_Panel[m_index].GetComponentInChildren<TouchMesh>().m_is_select = false;
                }
                GetComponent<BoxCollider>().enabled = true;
                return;
            }

            //ターゲットパネルの操作
            if (changer.m_Panel[m_target_index].GetComponentInChildren<TouchMesh>().m_is_select)
                changer.m_Panel[m_target_index].GetComponentInChildren<TouchMesh>().m_is_select = false;
            else
                changer.m_Panel[m_target_index].GetComponentInChildren<TouchMesh>().m_is_select = true;

            if (changer.m_Panel[m_target_index].GetComponentInChildren<TouchMesh>().m_is_select)
                changer.AddCount_SelectPanel();
            else
                changer.SubCount_SelectPanel();

            GetComponent<BoxCollider>().enabled = true;

            //var changer = this.transform.parent.parent.GetComponent<PanelChanger>();
            //ひどいからそのうちなおす
            //if (changer.m_current_Selectpanel == 2)
            //{
            //    if (m_is_select)
            //        changer.SubCount_SelectPanel();
            //    m_is_select = false;
            //    return;
            //}

            //if (changer.m_current_Selectpanel < 2)
            //{
            //    m_is_select = (m_is_select) ? false : true;

            //    if (m_is_select)
            //        changer.AddCount_SelectPanel();
            //    else
            //        changer.SubCount_SelectPanel();
            //}
        }
    }

#endif
}
