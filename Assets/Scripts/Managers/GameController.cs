using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //referenses
    Board m_gameBoard;
    Spawner m_spawner;
    Shape m_activeShape;


    //time
    [SerializeField][Range(0.01f, 0.5f)] float m_dropInterval = 0.1f;

    float m_timeToDrop;

    float m_timeToNextKeyLeftRight;

    [SerializeField][Range(0.02f, 1f)] float m_keyRepeatRateLeftRight = 0.25f;

    float m_timeToNextKeyDown;

    [SerializeField][Range(0.01f, 0.5f)] float m_keyRepeatRateDown = 0.01f;

    float m_timeToNextKeyRotate;

    [SerializeField][Range(0.02f, 1f)] float m_keyRepeatRateRotate = 0.25f;

    bool m_gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        m_spawner = GameObject.FindObjectOfType<Spawner>();
        m_gameBoard = GameObject.FindObjectOfType<Board>();

        m_timeToNextKeyDown = Time.time + m_keyRepeatRateDown;
        m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;
        m_timeToNextKeyRotate = Time.time + m_keyRepeatRateRotate;
        m_timeToDrop = Time.time;

        if(!m_gameBoard)
        {
            Debug.LogWarning("WARNING: board not defined!");
        }
        if (!m_spawner)
        {
            Debug.LogWarning("WARNING: spawner not defined!");
        }
        else
        {
            m_spawner.transform.position = Vectorf.Round(m_spawner.transform.position);

            if (!m_activeShape)
            {
                m_activeShape = m_spawner.SpawnShape();
            }
        }
    }

    
    // Update is called once per frame
    void Update()
    {
        if(!m_gameBoard || !m_activeShape || !m_spawner || m_gameOver)
        {
            return;
        }
        PlayerInput();
    }
    void PlayerInput()
    {
        // example of NOT using the Input Manager
        //if (Input.GetKey ("right") && (Time.time > m_timeToNextKey) || Input.GetKeyDown (KeyCode.RightArrow)) 

        if (Input.GetButton("MoveRight") && (Time.time > m_timeToNextKeyLeftRight) || Input.GetButtonDown("MoveRight"))
        {
            m_activeShape.MoveRight();
            m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveLeft();
            }

        }
        else if (Input.GetButton("MoveLeft") && (Time.time > m_timeToNextKeyLeftRight) || Input.GetButtonDown("MoveLeft"))
        {
            m_activeShape.MoveLeft();
            m_timeToNextKeyLeftRight = Time.time + m_keyRepeatRateLeftRight;

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.MoveRight();
            }

        }
        else if (Input.GetButtonDown("Rotate") && (Time.time > m_timeToNextKeyRotate))
        {
            m_activeShape.RotateRight();
            m_timeToNextKeyRotate = Time.time + m_keyRepeatRateRotate;

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                m_activeShape.RotateLeft();
            }

        }
        else if (Input.GetButton("MoveDown") && (Time.time > m_timeToNextKeyDown) || (Time.time > m_timeToDrop))
        {
            m_timeToDrop = Time.time + m_dropInterval;
            m_timeToNextKeyDown = Time.time + m_keyRepeatRateDown;

            m_activeShape.MoveDown();

            if (!m_gameBoard.IsValidPosition(m_activeShape))
            {
                if (m_gameBoard.isOverLimit(m_activeShape))
                {
                    m_activeShape.MoveUp();
                    m_gameOver = true;
                    Debug.Log("GG");
                }
                else
                {
                    LandShape();
                }
            }

        }
    }
    private void LandShape()
    {
        m_timeToNextKeyLeftRight = Time.time;
        m_timeToNextKeyDown = Time.time;
        m_timeToNextKeyRotate = Time.time;
        m_activeShape.MoveUp();
        m_gameBoard.StoreShapeInGrid(m_activeShape);
        m_activeShape = m_spawner.SpawnShape();
        m_gameBoard.ClearAllRows();
    }
    public void Restart()
    {
        Debug.Log("Restarted");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
