using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartSign : MonoBehaviour {

    public float FadeTime = 0.0f;
    public float AlphaCounter=0.0f;
    public bool IncFlag=false;
	// Use this for initialization
	void Start () {
        this.GetComponent<Image>().color = new Color(255, 255, 255, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
	}

    public bool AlphaIncrease()
    {
        if (IncFlag)
        {
            AlphaCounter += (60 / FadeTime) / (255 * 60);
            this.GetComponent<Image>().color = new Color(255, 255, 255, AlphaCounter);
        }
        if (AlphaCounter > 1.0f)
        {
            this.GetComponent<Image>().color = new Color(255, 255, 255, 0.0f);
            AlphaCounter = 0.0f;
            IncFlag = false;
            return true;
    }
        else
            return false;
    }
    public void AlphaIncrease_Begin()
    {
        IncFlag = true;

    }
}
