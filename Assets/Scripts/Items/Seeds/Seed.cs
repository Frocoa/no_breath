using UnityEngine;
using Assets.Scripts.Inventory;

[CreateAssetMenu(fileName = "Seed", menuName = "Item/Seed", order = 1)]
public class Seed : Item
{
    [SerializeField]
    private GameObject plant;

    public GameObject Plant => plant;

    public override void Use(GameObject player, Vector2 position)
    {
        ItemFactory.SpawnItem(plant, position);
    }
    public override void Use(GameObject player)
    {
        Vector3 position = player.transform.position;
        ItemFactory.SpawnItem(plant, position);
    }
}
