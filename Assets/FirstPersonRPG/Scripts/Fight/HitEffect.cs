using UnityEngine;

public class HitEffect : MonoBehaviour, IHitable
{
    public GameObject hitEffectObject;
    public string idHitSound;

    public void Hit(HitInfo hitInfo)
    {
        if (hitInfo.isShowEffect)
        {
            SoundController.Play(idHitSound, transform.position);
            if (hitEffectObject != null)
            {
                GameObject go = Instantiate(hitEffectObject, hitInfo.point, Quaternion.identity) as GameObject;
                Destroy(go, 3);
            }
        }
    }
}
