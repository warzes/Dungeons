using UnityEngine;

public class Flower : Enemy
{
    public GameObject bullet;
    public GameObject bulletInstancePosition;

    protected bool prevSeePlayer;

    protected override void Update()
    {
        base.Update();
        if (prevSeePlayer == false && seePlayer == true)
        {
            attackTime = Time.time;
        }
        if (seePlayer == true && Time.time - attackTime > attackCooldown)
        {
            animator.SetTrigger("IsAttack");
            attackTime = Time.time;
        }
        prevSeePlayer = seePlayer;
    }

    // by the animation event
    protected virtual void OnShoot()
    {
        Instantiate(bullet, bulletInstancePosition.transform.position, transform.rotation);
    }
}