using System;
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
    public List<Transform> tail;
    public GameObject foodPrefab;
    public GameObject tailPrefab;

    private Vector3 lastPos;

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
                head.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.LEFT:
                nexPos = Vector3.left;
                head.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Direction.RIGHT:
                nexPos = Vector3.right;
                head.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.UP:
                nexPos = Vector3.up;
                head.rotation = Quaternion.Euler(0, 0, -90);
                break;
        }

        nexPos *= step;
        lastPos = head.position;
        head.position += nexPos;

        foreach(Transform t in tail)
        {
            Vector3 temp = t.position;
            t.position = lastPos;
            lastPos = temp;
        }

        StartCoroutine(MoveSnake());
    }

    public void Eat()
    {
        Vector3 tailPosition = head.position;
        
        if(tail.Count > 0)
        {
            tailPosition = tail[tail.Count - 1].position;
        }

        GameObject temp = Instantiate(tailPrefab, tailPosition, transform.localRotation);
        tail.Add(temp.transform);
    }
}
