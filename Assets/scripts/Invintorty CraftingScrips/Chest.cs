using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject chestUIPrefab;
    [SerializeField] private Transform chestUIParent;

    [HideInInspector] public List<Slot> allChestSlots = new List<Slot>();
    [HideInInspector] public GameObject chestInstantiatedParent;

    public Transform slotHoverTransform;

    // Loot tables
    [Header("Loot Tables")]
    [SerializeField] private bool randomLoot;
    [SerializeField] private LootTable lootTable;

    private void Start()
    {
        GameObject chestSlots = Instantiate(chestUIPrefab, chestUIParent.position, chestUIParent.rotation, chestUIParent);

        foreach (Transform childSlot in chestSlots.transform.GetChild(1))
        {
            Slot childSlotScript = childSlot.GetComponent<Slot>();
            allChestSlots.Add(childSlotScript);

            childSlotScript.initialiseSlot(slotHoverTransform);
        }

        chestInstantiatedParent = chestSlots;
        chestInstantiatedParent.SetActive(false);

        if (randomLoot)
        {
            lootTable.InisitaliseLootTable();
            lootTable.SpawnLoot(allChestSlots);
        }
    }
}
