using UnityEngine;
using System.Collections;

public class EventPointHolder : MonoBehaviour {

    [SerializeField, HeaderAttribute("ウサギ出現位置")]
    private Transform[] m_usagi_SpornPoint;

    [SerializeField, HeaderAttribute("ウサギがフルーツ投げる場所")]
    private Transform[] m_usagi_TargetPoint;

    private int m_usagi_arraySize;

    [SerializeField, HeaderAttribute("カラス出現位置")]
    private Transform[] m_karasu_SpornPoint;

    [SerializeField, HeaderAttribute("カラスがフルーツ投げる場所")]
    private Transform[] m_karasu_TargetPoint;

    private int m_karasu_arraySize;

	// Use this for initialization
	void Start () {
        m_usagi_arraySize = m_usagi_SpornPoint.Length;
        m_karasu_arraySize = m_karasu_SpornPoint.Length;
	}
	
    public Transform[] GetUsagiPoint()
    {
        Transform[] ret = new Transform[2];
        int index = Random.Range(0, m_usagi_arraySize);

        ret[0] = m_usagi_SpornPoint[index];
        ret[1] = m_usagi_TargetPoint[index];
        return ret;

    }

    public Transform[] GetKarasuPoint()
    {
        Transform[] ret = new Transform[2];
        int index = Random.Range(0, m_karasu_arraySize);

        ret[0] = m_karasu_SpornPoint[index];
        ret[1] = m_karasu_TargetPoint[index];
        return ret;

    }
	// Update is called once per frame
	void Update () {
	
	}
}
