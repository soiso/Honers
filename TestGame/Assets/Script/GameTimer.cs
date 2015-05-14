using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class GameTimer : MonoBehaviour {

    [SerializeField]
    private Text m_text;

    [SerializeField,Range(0,120)]
    private int timer = 15;

   

	// Use this for initialization
	void Start () 
    {

        m_text.color = Color.black;
	}
	
	// Update is called once per frame
	void Update () {

        int current_Second = timer - (int)Time.time;
        
        if(current_Second < 10)
        {
            m_text.color = Color.red;
        }
        m_text.text = current_Second.ToString();
	}
}
