using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Transform food;
    public List<Transform> tail;
    public GameObject tailPrefab;

    private Vector3 lastPos;

    public int cols = 29;
    public int rows = 15;

    public Text txtScore;
    public Text txtRecord;
    public int score;
    public int record;

    public GameObject panelGameOver;
    public GameObject panelTitle;

    void Start()
    {
        StartCoroutine(MoveSnake());
        SetFood();
        record = PlayerPrefs.GetInt("Record");
        txtRecord.text = "Record: " + record.ToString();
        Time.timeScale = 0;
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
        switch (moveDirection)
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

        foreach (Transform t in tail)
        {
            Vector3 temp = t.position;
            t.position = lastPos;
            lastPos = temp;
            t.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }

        StartCoroutine(MoveSnake());
    }

    public void Eat()
    {
        Vector3 tailPosition = head.position;

        if (tail.Count > 0)
        {
            tailPosition = tail[tail.Count - 1].position;
        }

        GameObject temp = Instantiate(tailPrefab, tailPosition, transform.localRotation);
        tail.Add(temp.transform);
        score += 10;
        txtScore.text = "Score: " + score.ToString();
        SetFood();
    }

    void SetFood()
    {
        int x = UnityEngine.Random.Range((cols - 1) / 2 * -1, (cols - 1) / 2);
        int y = UnityEngine.Random.Range((rows - 1) / 2 * -1, (rows - 1) / 2);

        food.position = new Vector2(x * step, y * step);
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        panelGameOver.SetActive(true);
        if(score > record)
        {
            PlayerPrefs.SetInt("Record", score);
            txtRecord.text = "New Record: " + score.ToString();
        }
    }

    public void Play()
    {
        head.position = Vector3.zero;
        moveDirection = Direction.LEFT;

        foreach(Transform t in tail)
        {
            Destroy(t.gameObject);
        }

        tail.Clear();
        head.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        SetFood();
        score = 0;
        txtScore.text = "Score: 0";
        record = PlayerPrefs.GetInt("Record");
        txtRecord.text = "Record: " + record.ToString();
        panelGameOver.SetActive(false);
        panelTitle.SetActive(false);
        Time.timeScale = 1;
    }
}
