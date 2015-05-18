using UnityEngine;
using System.Collections;

public class TreeParametor : MonoBehaviour {

    [SerializeField, HeaderAttribute("回転速度")]
    public float m_rotate_speed = 1.2f;

    [SerializeField, HeaderAttribute("木ので成長しているフルーツの最大回転角度")]
    public float m_max_RotateAngle = 30f;

    [SerializeField, HeaderAttribute("どんぐりが出る確率")]
    public int m_donguri_Probability = 10;

    [SerializeField, HeaderAttribute("フィーバー時間")]
    public float m_feaverTime = 4.0f;

    public FruitInterFace.FRUIT_TYPE m_book_fruit;

    public int m_event_Affiliation{get ; private set;} //所属するイベントインデックス

    [SerializeField, HeaderAttribute("どんぐり作成可能かどうか")]
    public bool m_enable_Donguri = false;

    void Awake()
    {
        m_book_fruit = FruitInterFace.FRUIT_TYPE.error;
    }

    public void Set_EventAffiliation(int index)
    {
        m_event_Affiliation = index;
    }
}
