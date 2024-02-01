using UnityEngine;

public class Drop : MonoBehaviour, IInteractive
{
    public string itemId = "";
    public SpriteRenderer spriteRenderer;
    private Item item;

    public string Name { get { return item.name; } }
    public bool IsActive { get { return true; } }
    public InteractiveAction Action { get { return InteractiveAction.Take; } }

    void Start()
    {
        item = ItemsController.Instance.GetItemById(itemId);
        if (item == null)
        {
            Destroy(gameObject);
            return;
        }
        spriteRenderer.sprite = item.icon;
    }

    public void Use()
    {
        if (PlayerController.Instance.inventory.AddItem(item))
        {
            SoundController.Play("pick_up");
            Destroy(gameObject);
        }
        else
        {
            UIController.Instance.ShowError("Inventory is full");
        }
    }
}
