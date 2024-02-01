using UnityEngine;
using UnityEngine.UI;

public class InventoryScreen : Form
{
    public ItemSlot[] slots;
    public ItemSlot weaponSlot;
    public ItemSlot armorSlot;
    public ItemSlot helmetSlot;
    public ItemSlot amuletSlot;
    public ItemSlot ringSlot;
    public ItemSlot dropSlot;

    public Text levelText;
    public Text pointsText;

    public Slider strenghtSlider;
    public Slider staminaSlider;
    public Slider mindSlider;
    public Slider willpowerSlider;

    public Text strenghtText;
    public Text staminaText;
    public Text mindText;
    public Text willpowerText;

    public Button addStrenghtButton;
    public Button addStaminaButton;
    public Button addMindButton;
    public Button addWillpowerButton;

    public Text protectionPhysicalText;
    public Text protectionFireText;
    public Text protectionIceText;
    public Text protectionElectroText;

    public Text hintText;
    public Image dragIcon;
    public Color defaultTextColor = Color.grey;
    public Color highlightTextColor = Color.green;

    private ItemSlot hoveredSlot;

    void Start()
    {
        strenghtSlider.maxValue = ExperienceController.maxSkillLevel;
        staminaSlider.maxValue = ExperienceController.maxSkillLevel;
        mindSlider.maxValue = ExperienceController.maxSkillLevel;
        willpowerSlider.maxValue = ExperienceController.maxSkillLevel;
        addStrenghtButton.onClick.AddListener(OnAddStrenghtClick);
        addStaminaButton.onClick.AddListener(OnAddStaminaClick);
        addMindButton.onClick.AddListener(OnAddMindClick);
        addWillpowerButton.onClick.AddListener(OnAddWillpowerClick);
        hintText.text = "";

        weaponSlot.onDblClick += OnSlotDblClick;
        armorSlot.onDblClick += OnSlotDblClick;
        helmetSlot.onDblClick += OnSlotDblClick;
        amuletSlot.onDblClick += OnSlotDblClick;
        ringSlot.onDblClick += OnSlotDblClick;

        weaponSlot.onHover += OnSlotHover;
        armorSlot.onHover += OnSlotHover;
        helmetSlot.onHover += OnSlotHover;
        amuletSlot.onHover += OnSlotHover;
        ringSlot.onHover += OnSlotHover;
        dropSlot.onHover += OnSlotHover;

        foreach (ItemSlot itemSlot in slots)
        {
            itemSlot.onDblClick += OnSlotDblClick;
            itemSlot.onHover += OnSlotHover;
            itemSlot.onDrag += OnSlotDrag;
        }
        foreach (ItemSlot itemSlot in UIController.Instance.gameScreen.slots)
        {
            itemSlot.onDblClick += OnSlotDblClick;
            itemSlot.onHover += OnSlotHover;
            itemSlot.onDrag += OnSlotDrag;
        }
    }

    void Update()
    {
        if (IsShown == false) return;
        PlayerController player = PlayerController.Instance;

        levelText.text = "Level: " + player.experience.Level;
        pointsText.text = "Points: " + player.experience.Points;

        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rootPanel.GetComponent<RectTransform>(), Input.mousePosition, null, out localPoint);
        if (dragIcon.gameObject.activeSelf == true)
        {
            dragIcon.transform.parent.GetComponent<RectTransform>().anchoredPosition = localPoint;
        }

        strenghtSlider.value = player.experience.Strenght;
        staminaSlider.value = player.experience.Stamina;
        mindSlider.value = player.experience.Mind;
        willpowerSlider.value = player.experience.Willpower;

        strenghtText.text = player.experience.ActualStrenght.ToString();
        staminaText.text = player.experience.ActualStamina.ToString();
        mindText.text = player.experience.ActualMind.ToString();
        willpowerText.text = player.experience.ActualWillpower.ToString();
        strenghtText.color = player.experience.AdditionalStrenght > 0 ? highlightTextColor : defaultTextColor;
        staminaText.color = player.experience.AdditionalStamina > 0 ? highlightTextColor : defaultTextColor;
        mindText.color = player.experience.AdditionalMind > 0 ? highlightTextColor : defaultTextColor;
        willpowerText.color = player.experience.AdditionalWillpower > 0 ? highlightTextColor : defaultTextColor;

        addStrenghtButton.interactable = player.experience.Points > 0 && player.experience.Strenght < ExperienceController.maxSkillLevel;
        addStaminaButton.interactable = player.experience.Points > 0 && player.experience.Stamina < ExperienceController.maxSkillLevel;
        addMindButton.interactable = player.experience.Points > 0 && player.experience.Mind < ExperienceController.maxSkillLevel;
        addWillpowerButton.interactable = player.experience.Points > 0 && player.experience.Willpower < ExperienceController.maxSkillLevel;

        protectionPhysicalText.text = "Physical: " + (Mathf.RoundToInt(player.defence.physical * 100));
        protectionFireText.text = "Fire: " + (Mathf.RoundToInt(player.defence.fire * 100));
        protectionIceText.text = "Ice: " + (Mathf.RoundToInt(player.defence.ice * 100));
        protectionElectroText.text = "Electro: " + (Mathf.RoundToInt(player.defence.electro * 100));

        weaponSlot.SetItem(player.inventory.Weapon);
        armorSlot.SetItem(player.inventory.Armor);
        helmetSlot.SetItem(player.inventory.Helmet);
        amuletSlot.SetItem(player.inventory.Amulet);
        ringSlot.SetItem(player.inventory.Ring);
        foreach (ItemSlot slot in slots)
        {
            slot.SetItem(InventoryController.Instance.Items[slot.id]);
        }
    }

    public override void Show()
    {
        base.Show();
        hintText.text = "";
        dragIcon.gameObject.SetActive(false);
    }

    private void OnAddStrenghtClick()
    {
        PlayerController.Instance.experience.AddStrenght(1, false);
    }

    private void OnAddStaminaClick()
    {
        PlayerController.Instance.experience.AddStamina(1, false);
    }

    private void OnAddMindClick()
    {
        PlayerController.Instance.experience.AddMind(1, false);
    }

    private void OnAddWillpowerClick()
    {
        PlayerController.Instance.experience.AddWillpower(1, false);
    }

    private void OnSlotDblClick(ItemSlot slot)
    {
        if (slot == weaponSlot) { InventoryController.Instance.SetWeapon(null); return; }
        if (slot == armorSlot) { InventoryController.Instance.SetArmor(null); return; }
        if (slot == helmetSlot) { InventoryController.Instance.SetHelmet(null); return; }
        if (slot == amuletSlot) { InventoryController.Instance.SetAmulet(null); return; }
        if (slot == ringSlot) { InventoryController.Instance.SetRing(null); return; }
        InventoryController.Instance.UseItem(slot.id);
    }

    private void OnSlotHover(ItemSlot slot, bool state)
    {
        if (state == true)
        {
            hoveredSlot = slot;
            if (slot.ItemInSlot != null) hintText.text = slot.ItemInSlot.name + slot.ItemInSlot.description;
        }
        else
        {
            hoveredSlot = null;
            hintText.text = "";
        }
    }

    private void OnSlotDrag(ItemSlot slot, bool state)
    {
        if (state == true)
        {
            if (slot.ItemInSlot != null)
            {
                dragIcon.gameObject.SetActive(true);
                dragIcon.sprite = slot.ItemInSlot.icon;
            }
        }
        else
        {
            if (slot.ItemInSlot != null && hoveredSlot != null)
            {
                if (hoveredSlot.slotType == ItemSlot.SlotTypes.Drop)
                {
                    InventoryController.Instance.Drop(slot.id);
                }
                if (hoveredSlot.slotType == ItemSlot.SlotTypes.Backpack)
                {
                    InventoryController.Instance.Move(slot.id, hoveredSlot.id);
                }
            }
            dragIcon.gameObject.SetActive(false);
        }
    }
}
