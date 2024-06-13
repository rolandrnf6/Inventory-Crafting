using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Item : MonoBehaviour
{
    public new string name = "New Item";
    public string description = "New Description";
    public Sprite icon;
    public int currentQuantity = 1;
    public int maxQuantity = 16;

    public int equippableItemIndex = -1;

    [Header("Item Use")]
    public UnityEvent myEvent;
    public bool removeOneOnUse;

    private void Start()
    {
        UnityEvent newEvents = ItemEventManager.Instance.GetItemEvents(this);
        if (newEvents != null)
            myEvent = newEvents;
    }

    private void OnDisable()
    {
        if (!gameObject.scene.isLoaded && myEvent.GetPersistentEventCount() != 0)
        {
            return;
        }

        UnityEvent newEvents = ItemEventManager.Instance.GetItemEvents(this);
        if (newEvents != null)
            myEvent = newEvents;
    }

    public void UseItem()
    {
        if (myEvent.GetPersistentEventCount() > 0)
        {
            myEvent.Invoke();

            if (removeOneOnUse)
                currentQuantity--;
        }
    }
}
