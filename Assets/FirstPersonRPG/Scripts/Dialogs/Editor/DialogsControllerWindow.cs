using UnityEngine;
using UnityEditor;

public class DialogsControllerWindow : EditorWindow
{
    private DialogsLibrary library;
    private int index = 0;
    private Color defaultColor;

    [MenuItem("RPG/Dialogs")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<DialogsControllerWindow>();
    }

    private void OnGUI()
    {
        defaultColor = GUI.backgroundColor;
        if (DialogsLibrary.Instance == null) return;
        library = DialogsLibrary.Instance;

        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add Dialog")) library.AddDialog();
        GUI.backgroundColor = defaultColor;

        if (library.dialogs.Count == 0)
        {
            EditorGUILayout.EndHorizontal();
            return;
        }
        index = EditorGUILayout.Popup(index, library.GetDialogsId());
        index = Mathf.Clamp(index, 0, library.dialogs.Count - 1);
        Dialog dialog = library.dialogs[index];
        EditorGUILayout.LabelField("Id", GUILayout.Width(50));
        dialog.id = EditorGUILayout.TextField(dialog.id);

        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add Dialog Item"))
        {
            dialog.AddNewItem();
        }

        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Remove Dialog"))
        {
            library.RemoveDialog(dialog);
            return;
        }

        GUI.backgroundColor = defaultColor;
        if (GUILayout.Button("Save"))
        {
            PrefabUtility.ReplacePrefab(library.gameObject, PrefabUtility.GetPrefabParent(library.gameObject), ReplacePrefabOptions.ConnectToPrefab);
        }
        if (GUILayout.Button("Revert"))
        {
            PrefabUtility.ResetToPrefabState(library.gameObject);
        }
        EditorGUILayout.EndHorizontal();

        foreach (DialogItem item in dialog.dialogItems)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Icon", GUILayout.Width(50));
            item.icon = (Sprite)EditorGUILayout.ObjectField(item.icon, typeof(Sprite), false, GUILayout.Width(120));
            EditorGUILayout.LabelField("Name", GUILayout.Width(50));
            item.name = EditorGUILayout.TextField(item.name, GUILayout.Width(120));
            EditorGUILayout.LabelField("Text", GUILayout.Width(50));
            item.text = EditorGUILayout.TextField(item.text);

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove Dialog Item"))
            {
                dialog.Remove(item);
                return;
            }
            GUI.backgroundColor = defaultColor;
            EditorGUILayout.EndHorizontal();
        }
    }
}
