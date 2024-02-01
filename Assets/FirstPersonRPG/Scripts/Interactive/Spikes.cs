using UnityEngine;

public class Spikes : MonoBehaviour
{
    public float upDuration = 1;
    public float downDuration = 1;
    public DamageConfig damage;

    public bool IsUp { get; private set; }

    private float changeTime;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (IsUp == true && Time.time > changeTime + upDuration)
        {
            animator.SetBool("IsUp", false);
            IsUp = false;
            changeTime = Time.time;
        }
        if (IsUp == false && Time.time > changeTime + downDuration)
        {
            animator.SetBool("IsUp", true);
            IsUp = true;
            changeTime = Time.time;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        IHitable hitable = other.gameObject.GetComponent(typeof(IHitable)) as IHitable;
        if (hitable != null)
        {
            hitable.Hit(new HitInfo(damage, transform.position, false, HitInfo.HitSources.Enviroment));
        }
    }
}
