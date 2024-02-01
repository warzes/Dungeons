using UnityEngine;

public class Container : Destroyable
{
    public GameObject dropPrefab;
    public string itemId = "";

    protected override void Death()
    {
        if (itemId != "")
        {
            Drop drop = ((GameObject) Instantiate(dropPrefab, transform.position, Quaternion.identity)).GetComponent<Drop>();
            drop.itemId = itemId;
        }
        base.Death();
    }
}
