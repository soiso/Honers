using UnityEngine;
using System.Collections;

public class FieldObjectFrameInfomation : MonoBehaviour
{
    public enum movedirection { LEFT = -1, STAY, RIGHT = 1 };
    private Vector2 axis = Vector2.zero;
    public Vector2 GetAxis
    {
        get { return axis; }
    }
    private bool is_move = false;
    public bool Is_Move
    {
        get { return is_move; }
    }
    private movedirection move_Direction = movedirection.STAY;
    public movedirection MoveDirection
    {
        get { return move_Direction; }
    }
}

