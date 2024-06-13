using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealth : MonoBehaviour
{
    [SerializeField] private int currentHealth = 10;
    [SerializeField] private List<ItemDrop> itemDrops = new List<ItemDrop>();

    public void takeDamage(int damage, GameObject player)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            foreach (ItemDrop item in itemDrops)
            {
                int quantityToDrop = Random.Range(item.minQuantityToDrop, item.maxQuantityToDrop);

                if (quantityToDrop == 0) 
                    return;

                Item droppedItem = Instantiate(item.ItemToDRop,transform.position ,Quaternion.identity).GetComponent<Item>();

                player.GetComponent<Inventory>().addItemToInventory(droppedItem);
            }

            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class ItemDrop 
{
    public GameObject ItemToDRop;
    public int minQuantityToDrop = 1;
    public int maxQuantityToDrop = 5;
}
