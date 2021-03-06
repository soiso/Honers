﻿using UnityEngine;
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

    private int m_total_Num;

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
        //  m_total_Num++;
        //  int amari = m_total_Num % m_fruitmanager.Get_DongriCount;
        //if( amari == 0)
        //{
        //    m_fruitmanager.Create_Dongri();
        //}

        Objectmanager.m_instance.m_score.SetScore(score,Objectmanager.m_instance.m_scene_manager.currentScene_num);
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

    public void End_FeaverTime()
    {
        m_fruitmanager.End_FeaverTime();
    }

    private FruitInterFace.FRUIT_TYPE type;
    public FruitInterFace.FRUIT_TYPE SnatchFruit()
    {
        //フルーツ取ってなかったら帰ります
        if (m_total_Num <= 0) return FruitInterFace.FRUIT_TYPE.error;

        bool isok = true;
        
        while( isok )
        {
            type = (FruitInterFace.FRUIT_TYPE)Random.Range((int)(FruitInterFace.FRUIT_TYPE.apple), (int)(FruitInterFace.FRUIT_TYPE.num_normal_fruit));
            switch (type)
            {
                case FruitInterFace.FRUIT_TYPE.apple:
                    if (apple_num <= 0) break;
                    apple_num--;
                    isok = false;
                    break;
                case FruitInterFace.FRUIT_TYPE.strawberry:
                    if (strawberry_num <= 0) break;
                    strawberry_num--;
                    isok = false;
                    break;
                case FruitInterFace.FRUIT_TYPE.peach:
                    if (peach_num <= 0) break;
                    peach_num--;
                    isok = false;
                    break;
                case FruitInterFace.FRUIT_TYPE.grape:
                    if (grape_num <= 0) break;
                    grape_num--;
                    isok = false;
                    break;
            }
        }

        return type;
    }
}
