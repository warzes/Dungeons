using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject weaponHand;
    private ItemWeapon weapon;
    private Animator animator;
    private GameObject weaponModel;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameController.Instance.GameState != GameStates.Game) return;
        animator.SetBool("IsAttack", InputController.Weapon);
    }

    // by the animation event
    public void OnAttack()
    {
        if (weapon == null) return;
        PlayerController player = PlayerController.Instance;
        if (weapon.itemType == Item.ItemTypes.Staff)
        {
            ItemWeaponStaff staff = (ItemWeaponStaff)weapon;
            if (player.Mana >= staff.mana)
            {
                Instantiate(staff.bullet, player.mainCamera.transform.position, player.mainCamera.transform.rotation);
                player.Mana -= staff.mana;
            }
            else
            {
                UIController.Instance.ShowError("Not enought mana");
            }
        }
        else
        {
            RaycastHit hit;
            if (Physics.Raycast(player.mainCamera.transform.position, player.mainCamera.transform.TransformDirection(Vector3.forward), out hit, 4))
            {
                IHitable hitable = hit.collider.gameObject.GetComponent(typeof(IHitable)) as IHitable;
                if (hitable != null)
                {
                    DamageConfig damageWithStreanght = ((ItemWeaponMelee)weapon).damage.Copy();
                    damageWithStreanght.minPhysical += (int)(damageWithStreanght.minPhysical * 0.05f * PlayerController.Instance.experience.ActualStrenght);
                    damageWithStreanght.maxPhysical += (int)(damageWithStreanght.maxPhysical * 0.05f * PlayerController.Instance.experience.ActualStrenght);
                    hitable.Hit(new HitInfo(damageWithStreanght, hit.point, true, HitInfo.HitSources.Player));
                }
            }
        }
    }

    public void OnSetWeapon(ItemWeapon weapon)
    {
        this.weapon = weapon;
        animator.SetBool("IsAttack", false);
        if (weaponModel != null) Destroy(weaponModel);
        if (weapon == null)
        {
            animator.SetInteger("WeaponType", -1);
        }
        else
        {
            weaponModel = (GameObject)Instantiate(weapon.model);
            weaponModel.transform.parent = weaponHand.transform;
            weaponModel.transform.localPosition = Vector3.zero;
            weaponModel.transform.localRotation = Quaternion.identity;
            weaponModel.transform.localScale = Vector3.one;
            animator.SetInteger("WeaponType", ItemWeapon.GetAnimationId(weapon));
        }
    }
}

