using UnityEngine;
using System.Collections;

public class Touch_Collider : MonoBehaviour {

    public bool m_is_active{ get; set; }

    void Start()
    {
        m_is_active = false;
    }

    // Update is called once per frame


#if UNITY_STANDALONE
    void OnMouseDown()
    {
        m_is_active = true;
    }

    void OnMouseUp()
    {
        m_is_active = false;
    }

#elif UNITY_ANDROID || UNITY_IOS

    void Update()
    {
        IsTouch();
    }

    private Vector3 m_TouchStartPos;
    private Vector3 m_TouchEndPos;

    public void IsTouch()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;
        //    if (gameObject.GetComponent<MeshCollider>().Raycast(ray, out hit, 50.0f) == true)
        //    {
        //        m_is_active = (m_is_active) ? false : true;
        //    }
        //}
        if (Input.GetKey(KeyCode.Mouse0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (gameObject.GetComponent<MeshCollider>().Raycast(ray, out hit, 50.0f) == true)
            {
                m_is_active = true;
            }
            else m_is_active = false;
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            m_is_active = false;
        }
      
    }

#endif
}
