using UnityEngine;
using System.Collections;

public class FruitInfomation : MonoBehaviour {

    [SerializeField]
    private FruitInterFace.FRUIT_TYPE m_type;

    public FruitInterFace.FRUIT_TYPE fruit_type { get { return m_type; } }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
