using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MainGrid : MonoBehaviour
{
    Tilemap mainTilemap;

    [SerializeField] Tilemap backgroundLayer;
    [SerializeField] Tilemap objectsLayer;
    [SerializeField] Tilemap highLayer;
    public static MainGrid Instance { get; private set; }

    private readonly Dictionary<Vector3Int, WorldInteractable> objectMap = new();

    public void RegisterObject(Vector3 position, WorldInteractable interactable)
    {
        Vector3Int cellPosition = mainTilemap.WorldToCell(position);
        objectMap[cellPosition] = interactable;
    }

    public void UnregisterObject(Vector3Int cellPosition)
    {
        objectMap.Remove(cellPosition);
    }

    public WorldInteractable GetObjectAtPosition(Vector3 position)
    {
        Vector3Int cellPosition = mainTilemap.WorldToCell(position);
        if (objectMap.TryGetValue(cellPosition, out WorldInteractable interactable))
        {
            return interactable;
        }
        return null;
    }

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

    public Tilemap GetObjectsTileMap()
    {
        return objectsLayer;
    }


    public Vector3Int WorldToCellPosition(Vector3 worldPosition)
    {
        return mainTilemap.WorldToCell(worldPosition);
    }

    public Vector3 WorldToCell(Vector3 worldPosition)
    {
        Vector3Int cellPosition = mainTilemap.WorldToCell(worldPosition);
        return mainTilemap.GetCellCenterWorld(cellPosition);
    }

    public TileBase GetTileInBackgroundLayer(Vector3 position)
    {
        Vector3Int cellPosition = mainTilemap.WorldToCell(position);
        TileBase tile = backgroundLayer.GetTile(cellPosition);
        return tile;
    }

    public TileBase GetTileInObjectsLayer(Vector3 position)
    {
        Vector3Int cellPosition = mainTilemap.WorldToCell(position);
        TileBase tile = objectsLayer.GetTile(cellPosition);
        return tile;
    }

    public void SetTileInBackgroundLayer(Vector3 position, TileBase tile)
    {
        Vector3Int cellPosition = mainTilemap.WorldToCell(position);
        backgroundLayer.SetTile(cellPosition, tile);
    }

    public void SetGameObjectInObjectsLayer(Vector3 position, GameObject gameObject)
    {
        Vector3Int cellPosition = mainTilemap.WorldToCell(position);
        TileBase tile = objectsLayer.GetTile(cellPosition);
        if (tile != null)
        {
            // If there's already a tile, we can replace it or handle it as needed
            objectsLayer.SetTile(cellPosition, null);
        }
        Instantiate(gameObject, mainTilemap.GetCellCenterWorld(cellPosition), Quaternion.identity);
    }
}