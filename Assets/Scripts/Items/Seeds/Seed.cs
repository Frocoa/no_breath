using UnityEngine;
using Assets.Scripts.Inventory;

[CreateAssetMenu(fileName = "Seed", menuName = "Item/Seed", order = 1)]
public class Seed : Item
{
    [SerializeField]
    private Crop plant;

    public Crop Plant => plant;

    public override void UseCore(GameObject player, Vector2 position)
    {
        ItemFactory.SpawnItem(plant, position);
    }

    public override bool IsUsable(GameObject player, Vector2 position)
    {
        bool plantableTile = MainGrid.Instance.GetTileInBackgroundLayer(position) is PlantableTile;
        bool hasSpace = MainGrid.Instance.GetTileInObjectsLayer(position) == null;

        return plantableTile && hasSpace;
    }
}
