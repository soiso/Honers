using UnityEngine;
using System.Collections;

public class FruitInfomation : MonoBehaviour {

    [SerializeField]
    private Fruit.FRUIT_TYPE m_type;

    public Fruit.FRUIT_TYPE fruit_type { get { return m_type; } }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
