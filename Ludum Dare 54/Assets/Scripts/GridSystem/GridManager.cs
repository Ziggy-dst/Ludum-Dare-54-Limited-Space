using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public Vector2 startGridPos;
    public float cellSize;

    public GameObject gridPrefab;

    public int col;
    public int row;

    private Vector2 currentPos;

    public bool isHiding = false;

    // Start is called before the first frame update
    void Start()
    {
        currentPos = startGridPos;
        GenerateGrid();
        if (isHiding) gameObject.SetActive(false);
    }

    private void GenerateGrid()
    {

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Instantiate(gridPrefab, currentPos, Quaternion.identity, transform);
                currentPos.x += cellSize;
            }

            currentPos.x = startGridPos.x;
            currentPos.y -= cellSize;
        }
    }
}
