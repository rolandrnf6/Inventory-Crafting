using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemEventManager : MonoBehaviour
{
    private static ItemEventManager instance;

    [SerializeField] private List<ItemReferences> itemReferences = new List<ItemReferences>();

    public UnityEvent GetItemEvents(Item item)
    {
        ItemReferences itemRef = itemReferences.Find(x => x.item.name == item.name);
        return itemRef != null ? itemRef.events : null;
    }

    public static ItemEventManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ItemEventManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("ItemEventManager");
                    instance = obj.AddComponent<ItemEventManager>();
                }
            }

            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

[System.Serializable]
public class ItemReferences
{
    public Item item;
    public UnityEvent events;
}