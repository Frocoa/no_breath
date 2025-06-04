using UnityEngine;
using UnityEngine.Tilemaps;

public class MainGrid : MonoBehaviour
{
    Tilemap mainTilemap;

    [SerializeField] Tilemap backgroundLayer;
    [SerializeField] Tilemap objectsLayer;
    [SerializeField] Tilemap highLayer;
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

    public Tilemap GetObjectsTileMap()
    {
        return objectsLayer;
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