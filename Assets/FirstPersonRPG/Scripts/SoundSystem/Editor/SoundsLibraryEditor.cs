using UnityEngine;
using UnityEditor;

public class SoundsLibraryWindow : EditorWindow
{
    private SoundsLibrary library;
    private Color defaultColor;

    [MenuItem("RPG/Sounds")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<SoundsLibraryWindow>();
    }

    void OnGUI()
    {
        defaultColor = GUI.backgroundColor;
        if (SoundsLibrary.Instance == null) return;
        library = SoundsLibrary.Instance;

        EditorGUILayout.BeginHorizontal();
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add")) library.AddNewItem();
        GUI.backgroundColor = defaultColor;
        if (GUILayout.Button("Save")) PrefabUtility.ReplacePrefab(library.gameObject, PrefabUtility.GetPrefabParent(library.gameObject), ReplacePrefabOptions.ConnectToPrefab);
        if (GUILayout.Button("Revert")) PrefabUtility.ResetToPrefabState(library.gameObject);
        EditorGUILayout.EndHorizontal();

        for (int i = library.Items.Count - 1; i >= 0; i--)
        {
            SoundItem item = library.Items[i];

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("ID", GUILayout.Width(20));
            item.id = EditorGUILayout.TextField(item.id, GUILayout.Width(120));
            EditorGUILayout.LabelField("Clip", GUILayout.Width(50));
            item.clip = (AudioClip)EditorGUILayout.ObjectField(item.clip, typeof(AudioClip), false, GUILayout.Width(180));
            EditorGUILayout.LabelField("Volume", GUILayout.Width(60));
            item.volume = EditorGUILayout.Slider(item.volume, 0f, 1f);
            EditorGUILayout.LabelField("2D", GUILayout.Width(20));
            item.is2d = EditorGUILayout.Toggle(item.is2d, GUILayout.Width(20));

            GUI.backgroundColor = Color.red;
            if (GUILayout.Button("Remove", GUILayout.Width(120))) library.RemoveItem(item);
            GUI.backgroundColor = defaultColor;
            EditorGUILayout.EndHorizontal();
        }
    }
}
