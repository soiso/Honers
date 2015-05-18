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
    public virtual void Collision(GameObject col_object) { }
}
