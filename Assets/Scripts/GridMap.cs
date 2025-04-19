using UnityEngine;

public class GridMap
{

    private Color[,] gridArray;
    private readonly float cellSize;

    public GridMap(int width, int height, float cellSize)
    {
        this.cellSize = cellSize;

        gridArray = new Color[width, height];
        Debug.Log("Created grid with width: " + width + " and height: " + height);

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                gridArray[x, y] = Color.white;
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + 1), Color.white, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + 1, y), Color.white, 100f);
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * cellSize;
    }

    public Vector2 WorldPositionToGridPosition(Vector2 worldPosition)
    {
        int x = Mathf.FloorToInt(worldPosition.x / cellSize);
        int y = Mathf.FloorToInt(worldPosition.y / cellSize);

        return new Vector2(x, y);
    }

    public void SetGridColor(Vector2 gridPosition, Color color)
    {
        if (gridPosition.x >= 0 && gridPosition.x < gridArray.GetLength(0) && gridPosition.y >= 0 && gridPosition.y < gridArray.GetLength(1))
        {
            gridArray[(int)gridPosition.x, (int)gridPosition.y] = color;
            Debug.DrawLine(GetWorldPosition((int)gridPosition.x, (int)gridPosition.y), GetWorldPosition((int)gridPosition.x, (int)gridPosition.y + 1), color, 100f);
            Debug.DrawLine(GetWorldPosition((int)gridPosition.x, (int)gridPosition.y), GetWorldPosition((int)gridPosition.x + 1, (int)gridPosition.y), color, 100f);
            Debug.DrawLine(GetWorldPosition((int)gridPosition.x + 1, (int)gridPosition.y), GetWorldPosition((int)gridPosition.x + 1, (int)gridPosition.y + 1), color, 100f);
            Debug.DrawLine(GetWorldPosition((int)gridPosition.x, (int)gridPosition.y + 1), GetWorldPosition((int)gridPosition.x + 1, (int)gridPosition.y + 1), color, 100f);
        }
    }
}



