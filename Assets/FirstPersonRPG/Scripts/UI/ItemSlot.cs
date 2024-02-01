using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public enum SlotTypes
    {
        Backpack = 0,
        Equipment = 1,
        Drop = 2
    }

    public System.Action<ItemSlot> onDblClick;
    public System.Action<ItemSlot, bool> onHover;
    public System.Action<ItemSlot, bool> onDrag;

    public Image icon;
    public Text numText;
    public int id = 0;
    public bool showHotkeyLabel = false;
    public SlotTypes slotType = SlotTypes.Backpack;

    public Item ItemInSlot { get; private set; }

    void Start()
    {
        if (showHotkeyLabel == true)
        {
            numText.gameObject.SetActive(true);
            numText.text = (id + 1).ToString();
        }
        else
        {
            numText.gameObject.SetActive(false);
        }
    }

    public void SetItem(Item item)
    {
        ItemInSlot = item;
        if (item == null || item.id == "")
        {
            icon.gameObject.SetActive(false);
        }
        else
        {
            icon.gameObject.SetActive(true);
            icon.sprite = item.icon;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.clickCount == 2 && onDblClick != null)
        {
            onDblClick(this);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onHover != null)
        {
            onHover(this, true);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (onHover != null)
        {
            onHover(this, false);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onDrag(this, true);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onDrag(this, false);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
    }
}
