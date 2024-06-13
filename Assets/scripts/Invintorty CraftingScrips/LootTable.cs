using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChestLootTable", menuName = "Inventory/ChestLootTable")]
public class LootTable : ScriptableObject
{
    [System.Serializable]
    public class LootItem
    {
        public GameObject itemPrefab;
        public int minSpawn;
        public int maxSpawn;
        [Range(0f, 100f)] public float spawnChance;
    }

    public List<LootItem> lootItems = new List<LootItem>();
    [Range(0, 100)] public int spawnChancePerSlot = 20;

    public void InisitaliseLootTable()
    {
        float totalSpawnChance = 0f;
        foreach (LootItem item in lootItems)
        {
            totalSpawnChance += item.spawnChance;
        }

        if (totalSpawnChance > 100f)
        {
            NormaliseSpawnChances();
        }
    }

    private void NormaliseSpawnChances()
    {
        float normalisationFactor = 100f / CalculateTotalSpawnChance();
        foreach (LootItem item in lootItems)
        {
            item.spawnChance *= normalisationFactor;
        }
    }

    private float CalculateTotalSpawnChance()
    {
        float totalSpawnChance = 0f;
        foreach (LootItem item in lootItems)
        {
            totalSpawnChance += item.spawnChance;
        }

        return totalSpawnChance;
    }

    public void SpawnLoot(List<Slot> allChestSlots)
    {
        foreach (Slot chestSlot in allChestSlots)
        {
            if (Random.Range(0f, 100f) <= spawnChancePerSlot)
            {
                SpawnRandomItem(chestSlot);
            }
        }
    }

    private void SpawnRandomItem(Slot slot)
    {
        LootItem chosenItem = ChooseRandomItem();
        if (chosenItem != null)
        {
            int spawnCount = Random.Range(chosenItem.minSpawn, chosenItem.maxSpawn + 1);

            GameObject spawnedItem = Instantiate(chosenItem.itemPrefab, Vector3.zero, Quaternion.identity);
            spawnedItem.SetActive(false);

            Item itemComponent = spawnedItem.GetComponent<Item>();
            if (itemComponent != null)
                itemComponent.currentQuantity = spawnCount;

            slot.setItem(itemComponent);
            slot.updateData();
        }
    }

    private LootItem ChooseRandomItem()
    {
        float randomValue = Random.Range(0f, 100f);
        float cumulativeChance = 0f;

        foreach (LootItem item in lootItems)
        {
            cumulativeChance += item.spawnChance;
            if (randomValue <= cumulativeChance)
            {
                return item;
            }
        }

        return null;
    }
}
