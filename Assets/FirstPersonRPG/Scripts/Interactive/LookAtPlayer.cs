using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public bool lockAxisY = false;

    void Update()
    {
        Vector3 lookDir = PlayerController.Instance.transform.position - transform.position;
        if (lockAxisY)  lookDir.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDir);
    }
}
