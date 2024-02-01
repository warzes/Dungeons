using UnityEngine;

public class PlayerController : Singleton<PlayerController>, IHitable
{
    public ExperienceController experience;
    public Camera mainCamera;
    public WeaponController weaponController;
    public InventoryController inventory;
    public DefenceConfig defence;

    public float speed = 10f;
    public float rotationSpeed = 10f;
    public float baseManaRegeenrationDeleay = 0.5f;

    public int startMaxHealth = 100;
    public int startMaxMana = 100;
    private int maxHealth;
    private int maxMana;
    private int health;
    private int mana;

    public GameObject dropPrefab;

    private float manaRegenerationTime;
    private CharacterController moveController;

    public IInteractive InteractiveObject { get; private set; }
    public float HitTime { get; private set; }

    public int MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            if (Health > MaxHealth) Health = MaxHealth;
        }
    }

    public int MaxMana
    {
        get { return maxMana; }
        set
        {
            maxMana = value;
            if (Mana > MaxMana) Mana = MaxMana;
        }
    }

    public int Health
    {
        get { return health; }
        set
        {
            health = Mathf.Clamp(value, 0, maxHealth);
            if (health == 0) Death();
        }
    }

    public int Mana
    {
        get { return mana; }
        set
        {
            mana = Mathf.Clamp(value, 0, maxMana);
        }
    }

    void Awake()
    {
        HitTime = -10;
        maxHealth = startMaxHealth;
        maxMana = startMaxMana;
        Health = maxHealth;
        Mana = maxMana;
        moveController = GetComponent<CharacterController>();
        inventory.onSetWeapon += weaponController.OnSetWeapon;
    }

    void Update()
    {
        if (GameController.Instance.GameState != GameStates.Game) return;

        // Movement
        transform.Rotate(0, InputController.Horizontal * rotationSpeed * Time.deltaTime, 0);
        Vector3 moveDirection = new Vector3(InputController.LeftRight, -1, InputController.ForwardBack);
        moveDirection = transform.TransformDirection(moveDirection);
        moveController.Move(moveDirection * speed * Time.deltaTime);

        // Camera rotation
        Quaternion targetCameraRotation = mainCamera.transform.localRotation;
        targetCameraRotation *= Quaternion.Euler(InputController.Vertical * rotationSpeed * Time.deltaTime, 0f, 0f);
        targetCameraRotation = ClampRotationAroundXAxis(targetCameraRotation);
        mainCamera.transform.localRotation = targetCameraRotation;

        // Search for interactive objects
        InteractiveObject = null;
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out hit, 4))
        {
            InteractiveObject = hit.collider.gameObject.GetComponent(typeof(IInteractive)) as IInteractive;
            if (InteractiveObject != null && InteractiveObject.IsActive == false) InteractiveObject = null;
        }
        if ((InteractiveObject != null) && (InputController.Use))
        {
            InteractiveObject.Use();
        }

        // Mana regeneration;
        if (Time.time - manaRegenerationTime > baseManaRegeenrationDeleay - (baseManaRegeenrationDeleay * 0.05 * experience.Mind))
        {
            Mana++;
            manaRegenerationTime = Time.time;
        }
    }

    private void Death()
    {
        weaponController.OnSetWeapon(null);
        GameController.Instance.GameState = GameStates.Finish;
    }

    public void Hit(HitInfo hitInfo)
    {
        SoundController.Play("hit_player");
        int damage = defence.CalculateFinalDamage(hitInfo.damage);
        HitTime = Time.time;
        Health -= damage;
    }

    public void DropItem(string itemId)
    {
        Drop drop = ((GameObject)Instantiate(dropPrefab, transform.position, Quaternion.identity)).GetComponent<Drop>();
        drop.transform.Translate(0, -(moveController.height / 2 + 0.08f), 0); // 0.08 - Skin Width
        drop.transform.Translate(transform.forward*0.5f);
        drop.itemId = itemId;
    }

    private Quaternion ClampRotationAroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, -75, 75);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        return q;
    }
}
