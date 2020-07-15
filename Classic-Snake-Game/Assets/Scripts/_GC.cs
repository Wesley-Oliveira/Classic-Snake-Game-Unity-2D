using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _GC : MonoBehaviour
{
    public enum Direction
    {
        LEFT, UP, DOWN, RIGHT
    }

    public Direction moveDirection;
    public float delayStep;
    public float step;

    public Transform head;

    void Start()
    {
        StartCoroutine(MoveSnake());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            moveDirection = Direction.UP;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            moveDirection = Direction.LEFT;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            moveDirection = Direction.DOWN;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveDirection = Direction.RIGHT;
        }
    }

    IEnumerator MoveSnake()
    {
        yield return new WaitForSeconds(delayStep);

        Vector3 nexPos = Vector3.zero;
        switch(moveDirection)
        {
            case Direction.DOWN:
                nexPos = Vector3.down;
                break;
            case Direction.LEFT:
                nexPos = Vector3.left;
                break;
            case Direction.RIGHT:
                nexPos = Vector3.right;
                break;
            case Direction.UP:
                nexPos = Vector3.up;
                break;
        }

        nexPos *= step;
        head.position += nexPos;

        StartCoroutine(MoveSnake());
    }
}
