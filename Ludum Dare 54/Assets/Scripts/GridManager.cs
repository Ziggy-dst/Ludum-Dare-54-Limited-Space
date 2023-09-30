using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2 startGridPos;

    public GameObject gridPrefab;

    public int col;
    public int row;

    private Vector2 currentPos;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = startGridPos;
        GenerateGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateGrid()
    {
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Instantiate(gridPrefab, currentPos, Quaternion.identity);
                currentPos.x++;
            }

            currentPos.x = startGridPos.x;
            currentPos.y--;
        }
    }
}
