using UnityEngine;
using UnityEditor;

public class ItemsLibraryEditor : EditorWindow
{
    private ItemsController library;
    private Item.ItemTypes currentType = Item.ItemTypes.Quest;
    private Color defaultColor;

    [MenuItem("RPG/Items")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<ItemsLibraryEditor>();
    }

    void OnGUI()
    {
        defaultColor = GUI.backgroundColor;
        if (ItemsController.Instance == null) return;
        library = ItemsController.Instance;

        EditorGUILayout.BeginHorizontal();
        currentType = (Item.ItemTypes)EditorGUILayout.EnumPopup(currentType);
        GUI.backgroundColor = Color.green;
        if (GUILayout.Button("Add")) library.AddItem(currentType);
        GUI.backgroundColor = defaultColor;
        if (GUILayout.Button("Save")) PrefabUtility.ReplacePrefab(library.gameObject, PrefabUtility.GetPrefabParent(library.gameObject), ReplacePrefabOptions.ConnectToPrefab);
        if (GUILayout.Button("Revert")) PrefabUtility.ResetToPrefabState(library.gameObject);
        EditorGUILayout.EndHorizontal();

        for (int i = library.Items.Count - 1; i >= 0; i--)
        {
            Item item = library.Items[i];
            if (currentType == item.itemType)
            {
                DrawCommonProperties(item);
                if (currentType == Item.ItemTypes.Quest) DrawThingProperties((ItemQuest)item);
                if (currentType == Item.ItemTypes.WeaponMelee) DrawWeaponProperties((ItemWeaponMelee)item);
                if (currentType == Item.ItemTypes.Staff) DrawStaffProperties((ItemWeaponStaff)item);
                if (currentType == Item.ItemTypes.Potion) DrawPotionProperties((ItemPotion)item);
                if (currentType == Item.ItemTypes.Wearable) DrawWearableProperties((ItemWearable)item);
            }
        }
    }

    private void DrawCommonProperties(Item item)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("ID", GUILayout.Width(20));
        item.id = EditorGUILayout.TextField(item.id, GUILayout.Width(120));
        EditorGUILayout.LabelField("Name:", GUILayout.Width(40));
        item.name = EditorGUILayout.TextField(item.name, GUILayout.Width(120));
        EditorGUILayout.LabelField("Description: ", GUILayout.Width(70));
        item.description = EditorGUILayout.TextField(item.description);
        EditorGUILayout.LabelField("Icon", GUILayout.Width(50));
        item.icon = (Sprite)EditorGUILayout.ObjectField(item.icon, typeof(Sprite), false, GUILayout.Width(120));

        GUI.backgroundColor = Color.red;
        if (GUILayout.Button("Remove", GUILayout.Width(120))) library.RemoveItem(item);
        GUI.backgroundColor = defaultColor;
        EditorGUILayout.EndHorizontal();
    }

    private void DrawThingProperties(ItemQuest item) { }

    private void DrawWeaponProperties(ItemWeaponMelee item)
    {
        EditorGUILayout.BeginHorizontal();
        item.weaponMeleeType = (ItemWeaponMelee.WeaponMeleeTypes)EditorGUILayout.EnumPopup(item.weaponMeleeType, GUILayout.Width(120));
        EditorGUILayout.LabelField("Damage", GUILayout.Width(60));
        EditorGUILayout.LabelField("Phys", GUILayout.Width(30));
        item.damage.minPhysical = EditorGUILayout.IntField(item.damage.minPhysical, GUILayout.Width(30));
        EditorGUILayout.LabelField("-", GUILayout.Width(10));
        item.damage.maxPhysical = EditorGUILayout.IntField(item.damage.maxPhysical, GUILayout.Width(30));
        EditorGUILayout.LabelField(":: Fire", GUILayout.Width(50));
        item.damage.fire = EditorGUILayout.IntField(item.damage.fire, GUILayout.Width(30));
        EditorGUILayout.LabelField("Ice", GUILayout.Width(30));
        item.damage.ice = EditorGUILayout.IntField(item.damage.ice, GUILayout.Width(30));
        EditorGUILayout.LabelField("Electro", GUILayout.Width(50));
        item.damage.electro = EditorGUILayout.IntField(item.damage.electro, GUILayout.Width(30));
        EditorGUILayout.LabelField("Model", GUILayout.Width(50));
        item.model = (GameObject)EditorGUILayout.ObjectField(item.model, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();
    }

    private void DrawStaffProperties(ItemWeaponStaff item)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Mana", GUILayout.Width(60));
        item.mana = EditorGUILayout.IntField(item.mana, GUILayout.Width(60));
        EditorGUILayout.LabelField("Bullet", GUILayout.Width(60));
        item.bullet = (GameObject)EditorGUILayout.ObjectField(item.bullet, typeof(GameObject), false, GUILayout.Width(180));
        EditorGUILayout.LabelField("Model", GUILayout.Width(60));
        item.model = (GameObject)EditorGUILayout.ObjectField(item.model, typeof(GameObject), false);
        EditorGUILayout.EndHorizontal();
    }

    private void DrawWearableProperties(ItemWearable item)
    {
        EditorGUILayout.BeginHorizontal();
        item.wearableType = (ItemWearable.WearableTypes)EditorGUILayout.EnumPopup(item.wearableType, GUILayout.Width(120));
        EditorGUILayout.LabelField("Strenght", GUILayout.Width(60));
        item.strenghtBonus = EditorGUILayout.IntField(item.strenghtBonus, GUILayout.Width(30));
        EditorGUILayout.LabelField("Stamina", GUILayout.Width(60));
        item.staminaBonus = EditorGUILayout.IntField(item.staminaBonus, GUILayout.Width(30));
        EditorGUILayout.LabelField("Willpower", GUILayout.Width(60));
        item.willpowerBonus = EditorGUILayout.IntField(item.willpowerBonus, GUILayout.Width(30));
        EditorGUILayout.LabelField("Mind", GUILayout.Width(60));
        item.mindBonus = EditorGUILayout.IntField(item.mindBonus, GUILayout.Width(30));

        EditorGUILayout.LabelField(" DEFENSE (0-1):", GUILayout.Width(100));
        EditorGUILayout.LabelField("Physical", GUILayout.Width(60));
        item.defenceBonus.physical = EditorGUILayout.FloatField(item.defenceBonus.physical, GUILayout.Width(30));
        EditorGUILayout.LabelField("Fire", GUILayout.Width(60));
        item.defenceBonus.fire = EditorGUILayout.FloatField(item.defenceBonus.fire, GUILayout.Width(30));
        EditorGUILayout.LabelField("Ice", GUILayout.Width(60));
        item.defenceBonus.ice = EditorGUILayout.FloatField(item.defenceBonus.ice, GUILayout.Width(30));
        EditorGUILayout.LabelField("Electro", GUILayout.Width(60));
        item.defenceBonus.electro = EditorGUILayout.FloatField(item.defenceBonus.electro, GUILayout.Width(30));
        EditorGUILayout.EndHorizontal();
    }

    private void DrawPotionProperties(ItemPotion item)
    {
        EditorGUILayout.BeginHorizontal();
        item.potionType = (ItemPotion.PotionTypes)EditorGUILayout.EnumPopup(item.potionType, GUILayout.Width(120));
        if (item.potionType == ItemPotion.PotionTypes.Health)
        {
            EditorGUILayout.LabelField("Health", GUILayout.Width(60));
            item.healthPoints = EditorGUILayout.IntField(item.healthPoints, GUILayout.Width(60));
        }
        if (item.potionType == ItemPotion.PotionTypes.Mana)
        {
            EditorGUILayout.LabelField("Mana", GUILayout.Width(60));
            item.manaPoints = EditorGUILayout.IntField(item.manaPoints, GUILayout.Width(60));
        }
        if (item.potionType == ItemPotion.PotionTypes.Experience)
        {
            EditorGUILayout.LabelField("Exp", GUILayout.Width(60));
            item.experiencePoints = EditorGUILayout.IntField(item.experiencePoints, GUILayout.Width(60));
        }
        EditorGUILayout.EndHorizontal();
    }
}
