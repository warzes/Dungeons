using UnityEngine;

public class Bullet : MonoBehaviour
{
    public DamageConfig damage;
    public float speed = 10;
    public GameObject corpse;
    public HitInfo.HitSources hitSource;

    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        Move();
        CheckHit();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    protected virtual void CheckHit()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f, LayersHelper.DefaultEnemyPlayer))
        {
            IHitable hitable = hit.collider.gameObject.GetComponent(typeof(IHitable)) as IHitable;
            if (hitable != null)
            {
                hitable.Hit(new HitInfo(damage, hit.point, true, hitSource));
            }
            Death();
        }
    }

    protected virtual void Death()
    {
        if (corpse != null) Instantiate(corpse, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

}
