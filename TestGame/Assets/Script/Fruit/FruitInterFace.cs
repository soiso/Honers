using UnityEngine;
using System.Collections;

public class FruitInterFace : MonoBehaviour {

    public enum FRUIT_TYPE
    {
        error = -1,
        apple,
        strawberry,
        peach,
        grape,
        num_normal_fruit,
        
        donguri,
        speed_up,

    }
    [SerializeField, HideInInspector]
    public int m_event_Affiliation; //どのイベント木に所属するか

    [SerializeField, HideInInspector]
    public bool m_IsFeaverSporn; 

    public virtual void Collision(GameObject col_object) { }

    public bool Event()
    {
        return Objectmanager.m_instance.m_fruit_Counter.
            m_fruitmanager.Event(m_event_Affiliation);
    }
}
