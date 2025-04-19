using UnityEngine;
using Assets.Scripts.Inventory;

public class Seed : Item
{
    public GameObject Plant { get; private set; }

    public Seed(string name, GameObject plant) : base(name)
    {
        Plant = plant;
    }
    
    public override void Use()
    {
        Debug.Log($"Using item: {name}");
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
