using System.Collections.Generic;

[System.Serializable]
public class DialogsLibrary : Singleton<DialogsLibrary>
{
    public List<Dialog> dialogs;

    void Awake()
    {
        if (dialogs == null) dialogs = new List<Dialog>();
    }

    public Dialog AddDialog()
    {
        Dialog dialog = new Dialog();
        dialogs.Add(dialog);
        return dialog;
    }

    public void RemoveDialog(Dialog dialog)
    {
        dialogs.Remove(dialog);
    }

    public Dialog GetDialogById(string id)
    {
        Dialog dialog = dialogs.Find(d => d.id == id);
        return dialog;
    }

    public string[] GetDialogsId()
    {
        string[] strings = new string[dialogs.Count];
        for (int i = 0; i < dialogs.Count; i++)
        {
            strings[i] = dialogs[i].id;
        }
        return strings;
    }
}
