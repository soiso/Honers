using UnityEngine;
using System.Collections;

public class RandamRotate : MonoBehaviour {

    public enum ROTATETYPE
    {
        all,
        x,
        y,
        z,
    }

    Vector3 Create_Axis(ROTATETYPE type)
    {
        Vector3 ret = new Vector3(0, 0, 0);
        
        switch(type)
        {
            case ROTATETYPE.all :
                ret.x = Random.Range(0f, 1.0f);
                ret.y = Random.Range(0f, 1.0f);
                ret.z = Random.Range(0f, 1.0f);
                ret.Normalize();
                break;

            case ROTATETYPE.x :
                ret.x = 1.0f;
                break;

            case ROTATETYPE.y :
                ret.y = 1.0f;
                break;

            case ROTATETYPE.z :
                ret.z = 1.0f;
                break;
        }

        return ret;
    }


    public void Rotate(GameObject owner,ROTATETYPE type)
    {
        Vector3 axis = Create_Axis(type);

        float angle = Random.Range(0f, 360f);

        Quaternion rot = Quaternion.AngleAxis(angle, axis);
        owner.transform.rotation *= rot;
    }

}
