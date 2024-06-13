using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool hovered;
    private Item heldItem;

    private Color opaque = new Color(1, 1, 1, 1);
    private Color transparent = new Color(1, 1, 1, 0);

    private Image thisSlotImage;
    public TMP_Text thisSlotQuantityText;

    private Transform itemHoverInformation;

    public void initialiseSlot(Transform itemHoverTransform)
    {
        thisSlotImage = gameObject.GetComponent<Image>();
        thisSlotQuantityText = transform.GetChild(0).GetComponent<TMP_Text>();
        thisSlotImage.sprite = null;
        thisSlotImage.color = transparent;
        setItem(null);

        itemHoverInformation = itemHoverTransform;
    }

    public void setItem(Item item)
    {
        heldItem = item;

        if (item != null)
        {
            thisSlotImage.sprite = heldItem.icon;
            thisSlotImage.color = opaque;
            updateData();
        }
        else
        {
            thisSlotImage.sprite = null;
            thisSlotImage.color = transparent;
            updateData();
        }
    }

    public Item getItem()
    {
        return heldItem;
    }

    public bool hasItem()
    {
        return heldItem ? true : false;
    }

    public void updateData()
    {
        if (heldItem != null)
            thisSlotQuantityText.text = heldItem.currentQuantity.ToString();
        else
            thisSlotQuantityText.text = "";
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        hovered = true;

        if (itemHoverInformation && heldItem)
        {
            Vector3 newPos = new Vector3(transform.position.x + 80, transform.position.y + 80, 0);
            itemHoverInformation.position = newPos;
            itemHoverInformation.gameObject.SetActive(true);
            itemHoverInformation.GetComponentInChildren<TMP_Text>().text = "<b><u>" + heldItem.name + " x" + heldItem.currentQuantity + "</b></u> \n" + heldItem.description;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        hovered = false;

        if (itemHoverInformation)
        {
            itemHoverInformation.gameObject.SetActive(false);
        }
    }
}