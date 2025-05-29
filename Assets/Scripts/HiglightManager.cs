using UnityEngine;
using Assets.Scripts.Player;
using UnityEngine.Tilemaps;

public class HiglightManager : MonoBehaviour
{
    [SerializeField] private CharacterInputcontroller player;
    [SerializeField] private Tilemap buildingTilemap;


    // Update is called once per frame
    private Vector3Int? lastHighlightedCell = null;

    void Update()
    {
        if (player.HasItemInHand() && !player.IsInventoryOpen())
        {
            Vector2 mouseWorldPos = player.GetMousePosition();
            Vector3Int cellPosition = Vector3Int.FloorToInt(MainGrid.Instance.WorldToCell(mouseWorldPos));

            // Only update if the cell has changed
            if (lastHighlightedCell == null || lastHighlightedCell.Value != cellPosition)
            {

                // Remove highlight from previous cell
                if (lastHighlightedCell != null)
                {
                    buildingTilemap.SetTile(lastHighlightedCell.Value, null);
                }

                if (buildingTilemap.GetTile(cellPosition)) return;

                // Create a new 16x16 texture with the desired color and transparency
                var texture = new Texture2D(16, 16);
                Color32[] pixels = new Color32[16 * 16];
                Color32 highlightColor = new Color32(0, 255, 0, 64); // Green with 50% transparency
                for (int i = 0; i < pixels.Length; i++)
                    pixels[i] = highlightColor;
                texture.SetPixels32(pixels);
                texture.Apply();

                // Create a new sprite from the texture with pivot at the bottom center
                Sprite sprite = Sprite.Create(
                    texture,
                    new Rect(0, 0, 16, 16),
                    new Vector2(0.5f, 0f), // Pivot at bottom center
                    16
                );

                // Create a new tile and assign the sprite
                Tile tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = sprite;

                // Set the tile at the cell position
                buildingTilemap.SetTile(cellPosition, tile);

                lastHighlightedCell = cellPosition;
            }
        }
        else
        {
            // Remove highlight if mouse is not over the tilemap or player has no item
            if (lastHighlightedCell != null)
            {
                buildingTilemap.SetTile(lastHighlightedCell.Value, null);
                lastHighlightedCell = null;
            }
        }
    }
}
