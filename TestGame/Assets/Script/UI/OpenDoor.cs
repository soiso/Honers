﻿using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour
{
    [SerializeField]
    GameObject RightParent;
    [SerializeField]
    GameObject LeftParent;

    [SerializeField, Range(0, 1.0f)]
    private float rotate_speed = .0f;
    [SerializeField, Range(0, 180)]
    private int rotation_angle_max=0;

    private float rotation_angle;

    private bool rotation_flg = true;
    // Use this for initialization
    void Start()
    {
        rotation_angle = .0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotation_flg)
        {
            ParentRotation();
        }
    }

    private void ParentRotation()
    {
        rotation_angle = Mathf.Lerp(rotation_angle, rotation_angle_max, 0.01f);

        if (Mathf.DeltaAngle(RightParent.transform.eulerAngles.y, -rotation_angle_max) < -0.1f)
        {
            RightParent.transform.Rotate(new Vector3(0f, -rotate_speed, 0f));
        }
        if (Mathf.DeltaAngle(LeftParent.transform.eulerAngles.y, rotation_angle_max) > -0.1f)
        {
            LeftParent.transform.Rotate(new Vector3(0f, rotate_speed, 0f));
        }
    }
}
