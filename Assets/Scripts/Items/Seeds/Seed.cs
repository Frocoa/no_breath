using UnityEngine;
using Assets.Scripts.Inventory;

[CreateAssetMenu(fileName = "Seed", menuName = "Item/Seed", order = 1)]
public class Seed : Item
{
    [SerializeField]
    private GameObject plant;

    public GameObject Plant => plant;

    public override void Use()
    {
        Debug.Log($"Using item: {ItemName}");
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 position = player.transform.position;
            Object.Instantiate(Plant, position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }
}
