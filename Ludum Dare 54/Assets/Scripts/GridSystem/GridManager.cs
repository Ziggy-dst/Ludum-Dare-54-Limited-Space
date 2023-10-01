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

    // private List<GameObject> gridList;
    // private List<Vector2> gridPositionList;

    // Start is called before the first frame update
    void Start()
    {
        // GameManager.Instance.OnDragging +=
        currentPos = startGridPos;
        GenerateGrid();
        gameObject.SetActive(false);
    }

    private void GenerateGrid()
    {

        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                Instantiate(gridPrefab, currentPos, Quaternion.identity, transform);
                // gridList.Add(grid);
                // gridPositionList.Add(grid.transform.position);
                currentPos.x += cellSize;
            }

            currentPos.x = startGridPos.x;
            currentPos.y -= cellSize;
        }
    }

    // Vector2[] GetGridCoordinates(Vector3 snappedPosition, Vector2 objectSize)
    // {
    //     int startX = Mathf.FloorToInt((snappedPosition.x - objectSize.x / 2) / cellSize);
    //     int startY = Mathf.FloorToInt((snappedPosition.y - objectSize.y / 2) / cellSize);
    //     int endX = Mathf.CeilToInt((snappedPosition.x + objectSize.x / 2) / cellSize);
    //     int endY = Mathf.CeilToInt((snappedPosition.y + objectSize.y / 2) / cellSize);
    //
    //     Vector2[] coordinates = new Vector2[(endX - startX) * (endY - startY)];
    //     int index = 0;
    //     for (int x = startX; x < endX; x++)
    //     {
    //         for (int y = startY; y < endY; y++)
    //         {
    //             coordinates[index] = new Vector2(x, y);
    //             index++;
    //         }
    //     }
    //     return coordinates;
    // }
}
