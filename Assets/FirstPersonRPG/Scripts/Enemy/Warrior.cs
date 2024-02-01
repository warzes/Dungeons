using UnityEngine;

public class Warrior : Enemy
{
    public float attackDistance = 3;
    public DamageConfig damage;

    protected float seePlayerTime = -10;

    protected override void Update()
    {
        base.Update();
        transform.LookAt(PlayerController.Instance.transform);
        if (seePlayer) seePlayerTime = Time.time;
        if (seePlayer && distanceToPlayer < attackDistance && Time.time - attackTime > attackCooldown)
        {
            animator.SetTrigger("IsAttack");
            attackTime = Time.time;
        }
        else
        {
            if (Time.time - seePlayerTime < 5 && distanceToPlayer > attackDistance)
            {
                animator.SetBool("IsWalk", true);
                controller.Move((transform.forward - transform.up) * speed * Time.deltaTime);
            }
            else
            {
                animator.SetBool("IsWalk", false);
            }
        }
    }

    // by the animation event
    public void OnAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, attackDistance + 0.5f))
        {
            IHitable hitable = hit.collider.gameObject.GetComponent(typeof(IHitable)) as IHitable;
            if (hitable != null)
            {
                hitable.Hit(new HitInfo(damage, hit.point, false, HitInfo.HitSources.Enemy));
            }
        }
    }
}
