using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeaverSign : MonoBehaviour {

    //[SerializeField]
    private GameObject light;

    [SerializeField,Range(0,10.0f)]
    public float speed;

    [SerializeField, Range(0, 300.0f)]
    public float Height;

    [SerializeField, Range(0, 1.0f)]
    public float FadeSpeed;
    private Transform move_target;

    private Vector3 velocity;
    private bool feaver_flag;
	// Use this for initialization
	void Start () {
        //velocity = new Vector3(0, 0, 0);
        feaver_flag = false;
        this.GetComponent<Image>().color = new Color(255, 255, 255, 0.0f);
        light = GameObject.Find("Directional Light 1");
        light.GetComponent<Light>().intensity = 1;
        move_target = this.transform.Find("Target");
	}

    public void Reset()
    {
        feaver_flag = false;
        this.GetComponent<Image>().color = new Color(255, 255, 255, 0.0f);
    }

	
	// Update is called once per frame
    void Update()
    {
        if (feaver_flag)
        {
            this.GetComponent<RectTransform>().position += velocity;
            if(light.GetComponent<Light>().intensity > 0.1f)
            {
                light.GetComponent<Light>().intensity = Mathf.Lerp(light.GetComponent<Light>().intensity, 0.1f, FadeSpeed);
            }
        }
        else
        {
            if(light.GetComponent<Light>().intensity < 1.0f)
            {
                light.GetComponent<Light>().intensity = Mathf.Lerp(light.GetComponent<Light>().intensity, 1.0f, FadeSpeed);
            }
        }
    }

    public void Feaver_Begin()
    {
        feaver_flag = true;
        this.transform.position = new Vector3(700, Height, 0);
        velocity = new Vector3(-1.0f, 0, 0)*speed;
        this.GetComponent<Image>().color = new Color(255, 255, 255, 1.0f);
    }

    public void Feaver_End()
    {
        feaver_flag = false;
        this.GetComponent<Image>().color = new Color(255, 255, 255, 0.0f);
    }
}
