using UnityEngine;
using UnityEngine.Tilemaps;

public class Crop : MonoBehaviour
{

    [SerializeField]
    private Sprite[] growthStageSprites;
    private Tilemap tilemap;
    private int growthStage = 0;
    private readonly int timeToGrow = 5;
    private int stageProgress = 0;
    private readonly int maxGrowthStage = 5;
    private Vector3Int position;
    public void Initialize(Tilemap tilemap, Vector3 worldPosition)
    {
        this.tilemap = tilemap;
        position = tilemap.WorldToCell(worldPosition);


        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = growthStageSprites[growthStage];
        tilemap.SetTile(position, tile);
    }

    public void Remove()
    {
        // *shrug*
    }
    void Start()
    {
        TickManager.Instance.SubscribeToRandomTick(Grow);
    }
    private void Grow()
    {
        if (growthStage >= maxGrowthStage) return;

        stageProgress++;
        if (stageProgress >= timeToGrow)
        {
            growthStage++;
            stageProgress = 0;
            Tile t = ScriptableObject.CreateInstance<Tile>();
            t.sprite = growthStageSprites[growthStage];
            tilemap.SetTile(position, t);
        }
    }
}
