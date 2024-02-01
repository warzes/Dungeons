using System.Collections.Generic;

[System.Serializable]
public class Dialog
{
    public string id = "dialog";
    public List<DialogItem> dialogItems;

    public Dialog()
    {
        id = "Dialog " + this.GetHashCode();
        dialogItems = new List<DialogItem>();
    }

    public DialogItem AddNewItem()
    {
        DialogItem item = new DialogItem();
        dialogItems.Add(item);
        return item;
    }

    public void Remove(DialogItem item)
    {
        dialogItems.Remove(item);
    }

    public override string ToString()
    {
        return id;
    }
}
