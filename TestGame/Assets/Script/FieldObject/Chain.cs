using UnityEngine;
using System.Collections;

public class Chain : MonoBehaviour
{
    [SerializeField, HeaderAttribute("UVアニメーションスピード"), Range( .0f, 1.0f )]
    public float m_Speed;

    [SerializeField, HeaderAttribute("動かす元？(ハムスターとか)")]
    GameObject m_Owner;

    private float m_Current_Offset;

	// Use this for initialization
	void Start () 
    {
        m_Current_Offset = .0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        var obj = m_Owner.GetComponent<Hamster>();
        if( obj.m_time_Zone == PanelParametor.TIMEZONE.night )
        {
            UVAnimation( -m_Speed );
        }
        else
        {
            //UVAnimation( m_Speed );
        }
	}

    void UVAnimation( float speed )
    {
        m_Current_Offset += speed;

        MeshRenderer[] renderer = GetComponentsInChildren<MeshRenderer>();
        foreach( MeshRenderer r in renderer )
        {
            Material m = r.material;
            m.SetTextureOffset("_MainTex", new Vector2(m_Current_Offset, 1.0f));
        }

    }

}
