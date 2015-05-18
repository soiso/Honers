using UnityEngine;
using UnityEngine.UI;
using System.Collections;

using System.Collections.Generic;
using System.Linq;

public class FruitCounter : MonoBehaviour
{

    public int apple_num=0;
    public int strawberry_num=0;
    public int peach_num=0;
    public int grape_num = 0;
    public int m_dongri_Num { get; private set; }

    private Transform[] transformList;

    private Text scoretext;

    public FruitArrangeManager m_fruitmanager { get; private set; }

    void Start()
    {
        transformList = this.transform.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Transform transform in transformList)
        {
            if (transform.name == "FruitCounter") continue;
            if (transform.name == "Text") continue;
            if (transform.name == "Score") continue;
            if (transform.name == "Score_num")continue;
            GameObject fruit_num =transform.FindChild("Text").gameObject;
            scoretext = fruit_num.GetComponent<Text>();
            if (transform.name == "apple")
                scoretext.text = apple_num.ToString();
            if (transform.name == "peach")
                scoretext.text = peach_num.ToString();
            if (transform.name == "orrange")
                scoretext.text = grape_num.ToString();
            if (transform.name == "strawberry")
                scoretext.text = strawberry_num.ToString();
        }

        Debug.Log("dongri" + m_dongri_Num);
    }
    public void Reset()
    {
        apple_num = 0;
        strawberry_num = 0;
        peach_num = 0;
        grape_num = 0;
    }
    public void GetFruitType(FruitInterFace.FRUIT_TYPE type, float score)
    {
        
        switch(type)
        {
            case FruitInterFace.FRUIT_TYPE.apple:
                apple_num++;
                break;
            case FruitInterFace.FRUIT_TYPE.strawberry:
                strawberry_num++;
                break;
            case FruitInterFace.FRUIT_TYPE.peach:
                peach_num++;
                break;
            case FruitInterFace.FRUIT_TYPE.grape:
                grape_num++;
                break;
            //case FruitInterFace.FRUIT_TYPE.donguri:
            //    m_dongri_Num++;
            //    if(m_dongri_Num ==1)
            //    {
            //     
            //        m_dongri_Num = 0;
            //    }
            //    break;
        }
        Objectmanager.m_instance.m_score.SetScore(score);
    }

    public bool Set_FruitManager(FruitArrangeManager m)
    {
        m_fruitmanager = m;
        return true;
    }

    public void Bagin_FeaverTime()
    {
        m_fruitmanager.Begin_FeaverTime();
    }

}
