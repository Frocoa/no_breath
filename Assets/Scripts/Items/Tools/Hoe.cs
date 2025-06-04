using Assets.Scripts.Inventory;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "Seed", menuName = "Item/Tool/Hoe", order = 1)]
public class Hoe : Item
{
    [SerializeField] private PlantableTile tilledDirt;

    public override bool IsUsable(GameObject player, Vector2 position)
    {
        bool tillableTile = MainGrid.Instance.GetTileInBackgroundLayer(position) is not PlantableTile;
        bool hasSpace = MainGrid.Instance.GetTileInObjectsLayer(position) == null;
        return tillableTile && hasSpace;
    }

    public override void UseCore(GameObject player, Vector2 position)
    {
        MainGrid.Instance.SetTileInBackgroundLayer(position, tilledDirt);
    }
}
