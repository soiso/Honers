using UnityEngine;
using System.Collections;

public class FruitVaccumer : MonoBehaviour {

    [SerializeField, HeaderAttribute("吸い込む範囲")]
    private float m_range = 5.0f;
    [SerializeField, HeaderAttribute("吸い込む時間")]
    private float m_vaccum_Time = 3.0f;

    [SerializeField, HeaderAttribute("吸引力")]
    private float m_power = 2.0f;

    private int[] m_vaccum_FruitArray;
    private float m_begin_Time;
    private GameObject m_fruit_Root;
    private bool m_isVaccumNow = false;

    void Awake()
    {
        m_vaccum_FruitArray = new int[(int)FruitInterFace.FRUIT_TYPE.num_normal_fruit];
    }

	void Start () 
    {
        m_fruit_Root =
            Objectmanager.m_instance.m_fruit_Counter.m_fruitmanager.m_fieldFruit_Root;

        m_vaccum_FruitArray = new int[(int)FruitInterFace.FRUIT_TYPE.num_normal_fruit];
        
    }

    private void Reset()
    {
        for (int i = 0 ; i < m_vaccum_FruitArray.Length; i++)
        {
            m_vaccum_FruitArray[i] = 0;
        }
        m_isVaccumNow = false;
    }

    private void Vaccum_Fruit()
    {
        for(int i = 0 ; i < m_fruit_Root.transform.childCount ; i++)
        {
            GameObject target = m_fruit_Root.transform.GetChild(i).gameObject;
            
            //距離判定
            Vector3 vec = this.transform.position - target.transform.position;
            if (vec.magnitude > m_range)
                continue;

            Vector3 velocity = vec.normalized * m_power * Time.deltaTime;

            target.transform.position += velocity;
            
        }
        if(m_begin_Time + m_vaccum_Time <= Time.time )
        {
            m_isVaccumNow = false;
            var spout_script = GetComponent<FruitSpout>();
            spout_script.Receive_Fruit(ref m_vaccum_FruitArray);
        }
    }
	
  public  void Begin_Vaccum()
    {
        m_isVaccumNow = true;
        m_begin_Time = Time.time;
    }

	void Update ()
    {


	    if(m_isVaccumNow)
        {
             Vaccum_Fruit();
         }
	}

    public void OnTriggerEnter(Collider other)
    {

        if (!m_isVaccumNow)
            return;
        var script = other.GetComponent<FruitInfomation>();
        if (!script)
            return;
        int type = (int)script.fruit_type;

        m_vaccum_FruitArray[type]++;
        DestroyObject(other.gameObject);
    }
}
