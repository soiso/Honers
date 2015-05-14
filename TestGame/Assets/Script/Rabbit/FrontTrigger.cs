using UnityEngine;
using System.Collections;

public class FrontTrigger : FieldObjectInterface {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider col_object)
    {
        //レイヤー名取得
        string layer_name = LayerMask.LayerToName(col_object.gameObject.layer);
        if (layer_name == "Panel")
        {
            var obj = col_object.GetComponent<Panel>();
            var param = col_object.GetComponent<PanelParametor>();

            if (param.GetTimezone == PanelParametor.TIMEZONE.morning)
            {
                //Set_TimeZone(time_zone.morning);
            }
            else if (param.GetTimezone == PanelParametor.TIMEZONE.noon)
            {
                //Set_TimeZone(time_zone.noon);
            }
            else if (param.GetTimezone == PanelParametor.TIMEZONE.night)
            {
                //Set_TimeZone(time_zone.night);
            }
        }
    }
}
