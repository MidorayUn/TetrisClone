using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform m_emptySprite;
    public int m_height = 30;
    public int m_weidht = 10;
    public int m_header = 8;

    Transform[,] m_grid;

    private void Awake()
    {
        m_grid = new Transform[m_height,m_weidht];
    }
    void Start()
    {
        DrawEmptyCells();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DrawEmptyCells()
    {
        if (m_emptySprite != null)
        {
            for (int y = 0; y < m_height - m_header; y++)
            {
                for (int x = 0; x < m_weidht; x++)
                {
                    Transform clone;
                    clone = Instantiate(m_emptySprite, new Vector3(x, y, 0), Quaternion.identity) as Transform;
                    clone.name = "Broad space (x = " + x.ToString() + ", y =  " + y.ToString() + ")";
                    clone.transform.parent = transform;
                }
            }
        }
        else
        {
            Debug.Log("³������ ����");
        }

    }
}
