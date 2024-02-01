using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour, IHitable
{
    public UnityEvent onDeath;

    public SpriteRenderer spriteRenderer;
    public int health = 100;
    public float speed = 10;
    public DefenceConfig defence = new DefenceConfig();
    public GameObject corpse;
    public float attackCooldown = 1;
    public GameObject hitEffectObject;
    public int experience = 100;
    public string idHitSound;

    protected CharacterController controller;
    protected bool seePlayer = false;
    protected float distanceToPlayer;
    protected float attackTime;
    protected float hitTime;
    protected Animator animator;

    protected virtual void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    protected virtual void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, PlayerController.Instance.transform.position);
        Vector3 lookDir = PlayerController.Instance.transform.position - (transform.position);
        lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);

        RaycastHit hit;
        seePlayer = false;
        if (PlayerController.Instance.Health > 0)
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 100, LayersHelper.DefaultEnemyPlayer))
            {
                if (hit.collider.gameObject.tag == "Player") seePlayer = true;
            }
        }

        //Hit Effect
        if (Time.time - hitTime < 0.5)
        {
            spriteRenderer.color = Color.Lerp(Color.red, Color.white, (Time.time - hitTime) * 2);
        }
    }

    protected virtual void Death()
    {
        if (corpse != null)
        {
            Instantiate(corpse, transform.position, transform.rotation);
        }
        onDeath.Invoke();
        Destroy(gameObject);
    }

    public void Hit(HitInfo hitInfo)
    {
        if (health <= 0) return;
        int dmg = defence.CalculateFinalDamage(hitInfo.damage);
        health -= dmg;
        hitTime = Time.time;
        SoundController.Play(idHitSound, transform.position);

        if (hitInfo.isShowEffect && hitEffectObject != null)
        {
            GameObject hitEffect = Instantiate(hitEffectObject, hitInfo.point, Quaternion.identity) as GameObject;
            Destroy(hitEffect, 3);
        }

        if (health <= 0)
        {
            if (hitInfo.source == HitInfo.HitSources.Player) PlayerController.Instance.experience.AddExperience(experience);
            Death();
        }
    }
}
