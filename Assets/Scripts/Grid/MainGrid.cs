using UnityEngine;
using UnityEngine.Tilemaps;

public class MainGrid : MonoBehaviour
{
    Tilemap mainTilemap;
    public static MainGrid Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        mainTilemap = GetComponentInChildren<Tilemap>();
    }

    
    public Vector3 WorldToCell(Vector3 worldPosition)
    {
        Vector3Int cellPosition = mainTilemap.WorldToCell(worldPosition);
        return mainTilemap.GetCellCenterWorld(cellPosition);
    }
}