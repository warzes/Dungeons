using UnityEngine;

public class Destroyable : MonoBehaviour, IHitable
{
    public int health;
    public DefenceConfig defence;
    public GameObject corpse;
    public GameObject hitEffectObject;
    public string idHitSound;

    public void Hit(HitInfo hitInfo)
    {
        if (health<=0) return;
        int damage = defence.CalculateFinalDamage(hitInfo.damage);
        health -= damage;
        SoundController.Play(idHitSound, transform.position);

        if (hitInfo.isShowEffect && damage > 0)
        {
            if (hitEffectObject != null)
            {
                GameObject hitEffect = Instantiate(hitEffectObject, hitInfo.point, Quaternion.identity) as GameObject;
                Destroy(hitEffect, 3);
            }
        }

        if (health <= 0) Death();
    }

    protected virtual void Death()
    {
        if (corpse != null)
        {
            Instantiate(corpse,transform.position,transform.rotation);
        }
        Destroy(gameObject);
    }
}
