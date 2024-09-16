using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //referenses
    Board m_gameBoard;
    Spawner m_spawner;
    Shape m_activeShape;


    //time
    float m_dropInterval = 0.25f; 

    float m_timeToDrop;




    // Start is called before the first frame update
    void Start()
    {
        m_spawner = GameObject.FindObjectOfType<Spawner>();
        m_gameBoard = GameObject.FindObjectOfType<Board>();

        if (m_spawner)
        {
            if(m_activeShape == null)
            {
                m_activeShape = m_spawner.SpawnShape();
            }
            m_spawner.transform.position = Vectorf.Round(m_spawner.transform.position);
        }

        if(!m_gameBoard)
        {
            Debug.LogWarning("WARNING: board not defined!");
        }
        if (!m_spawner)
        {
            Debug.LogWarning("WARNING: spawner not defined!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_gameBoard && !m_spawner)
        {
            return;
        }
        if(Time.time > m_timeToDrop)
        {
            m_timeToDrop = Time.time + m_dropInterval;
            if (m_activeShape)
            {
                m_activeShape.MoveDown();
                if (!m_gameBoard.isValidPosition (m_activeShape))
                {
                    m_activeShape.MoveUp();

                    if (m_spawner)
                    {
                        m_activeShape = m_spawner.SpawnShape();
                    }
                }
            }
        }

    }
}
