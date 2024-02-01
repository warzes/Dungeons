using System.Collections.Generic;

public class SoundsLibrary : Singleton<SoundsLibrary>
{
    public List<SoundItem> items = new List<SoundItem>();

    public List<SoundItem> Items { get { return items; } }

    public SoundItem AddNewItem()
    {
        SoundItem item = new SoundItem();
        items.Add(item);
        return item;
    }

    public void RemoveItem(SoundItem item)
    {
        items.Remove(item);
    }

    public SoundItem GetItemById(string id)
    {
        if (string.IsNullOrEmpty(id)) return null;
        SoundItem item = items.Find(i => i.id == id);
        return item;
    }
}