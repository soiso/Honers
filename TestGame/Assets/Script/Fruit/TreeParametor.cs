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

}
