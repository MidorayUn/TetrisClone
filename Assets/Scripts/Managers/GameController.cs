using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //referenses
    Board m_gameBoard;
    Spawner m_spawner;

    // Start is called before the first frame update
    void Start()
    {
        m_spawner = FindObjectOfType<Spawner>();
        m_gameBoard = FindObjectOfType<Board>();

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
        
    }
}
