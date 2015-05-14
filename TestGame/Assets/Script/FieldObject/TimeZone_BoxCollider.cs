using UnityEngine;
using System.Collections;


//RigitBody!!!!

public class TimeZone_BoxCollider : MonoBehaviour {

    public PanelParametor.TIMEZONE m_myColliderTimeZone { get; private set; }

    public bool m_is_field_Out { get; private set; }
    public bool m_touch_FieldObject { get; private set; }
    public string m_touch_ObjectLayerName { get; private set; }
    public bool m_touch_MainStageObject { get; private set; }

	void Start ()
    {
        m_is_field_Out = false;
        m_touch_FieldObject = false;
        m_touch_ObjectLayerName = "";
        m_touch_MainStageObject = false;

	}

   void Calculate_MyTimeZone()
    {

    }

	void Update () 
    {
	
	}

    void OnTriggerEnter(Collider col_object)
    {
        string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
        if(layer_name == "Panel")
        {
            
            m_myColliderTimeZone = col_object.GetComponent<PanelParametor>().GetTimezone;
        }
        if (layer_name == "FieldOutTrigger")
        {
            m_is_field_Out = true;
        }

        if(layer_name == "FieldObject")
        {
            m_touch_FieldObject = true;
            m_touch_ObjectLayerName = layer_name;
        }
        
        if(layer_name == "MainStageObject")
        {
            m_touch_MainStageObject = true;
        }

    }

     void    OnTriggerStay(Collider col_object)
    {



        string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
        if (layer_name == "Panel")
        {
            
            m_myColliderTimeZone = col_object.GetComponent<PanelParametor>().GetTimezone;
        }
        if (layer_name == "FieldOutTrigger")
        {
            m_is_field_Out = true;
        }

        if (layer_name == "FieldObject")
        {
            m_touch_FieldObject = true;
            m_touch_ObjectLayerName = layer_name;
        }

        if (layer_name == "MainStageObject")
        {
            m_touch_MainStageObject = true;
        }
    }
    

    void    OnTriggerExit(Collider col_object)
     {
         string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
         if (layer_name == "FieldOutTrigger")
         {
             m_is_field_Out = false;
         }

         if (layer_name == "FieldObject")
         {
             m_touch_FieldObject = false ;
             m_touch_ObjectLayerName = "";
         }

         if (layer_name == "MainStageObject")
         {
             m_touch_MainStageObject = false;
         }
     }
}
