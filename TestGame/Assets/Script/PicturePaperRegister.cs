
using UnityEngine;
using System.Collections;

public class PicturePaperRegister : MonoBehaviour {

    [SerializeField, HeaderAttribute("そのうちちゃんとしたやつにする")]
    private string m_picturePaperName = "PicturePaper";

    [SerializeField]
    GameObject pic_paper_prefab;

    private GameObject picpaper;

    void Awake()
    {
     GameObject paperObj = GameObject.Find(m_picturePaperName);
        if(!paperObj)
        {
            Debug.Log("PicturePaper is Not GameScene !!");

            picpaper = Instantiate(pic_paper_prefab);
            picpaper.name = pic_paper_prefab.name;
            picpaper.GetComponent<PicturePaper>().SetMove_Target("MoveTarget");
            paperObj = picpaper;


        }
        this.transform.SetParent(paperObj.transform);
    }

	void Start ()
    {

   
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
}
//=======
//﻿using UnityEngine;
//using System.Collections;

//public class PicturePaperRegister : MonoBehaviour
//{

//    private GameObject manager;
//    // Use this for initialization
//    void Start()
//    {
//        manager = GameObject.Find("SceneManager");
//        GameObject paperObj = GameObject.Find("PicturePaper" + manager.GetComponent<SceneManager>().GetCurrentSceneName());
//        if (!paperObj)
//        {
//            Debug.Log("PicturePaper is Not GameScene !!");
//            paperObj = manager.GetComponent<SceneManager>().PicPaperFind(manager.GetComponent<SceneManager>().GetCurrentSceneName());
//        }
//        this.transform.SetParent(paperObj.transform);
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }
//}
//>>>>>>> d4b2b6f2d701d63d977169c566a8fe345576e3a8
