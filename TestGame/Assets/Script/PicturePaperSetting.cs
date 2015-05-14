using UnityEngine;
using System.Collections;

public class PicturePaperSetting : MonoBehaviour {
    private bool flag=false;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
    void Update()
    {
        if (flag == false)
        {
            this.transform.position = new Vector3(-3f, 5.3f, 20.0f);
            this.transform.localScale = new Vector3(1.01f, 1.28775f, 1.01f);
            flag = true;
        }
    }
}
