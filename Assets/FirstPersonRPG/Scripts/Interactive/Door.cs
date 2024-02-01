using UnityEngine;

class Door : MonoBehaviour, IInteractive
{
    public string keyId = "";
    public bool isInteractive = true;

    private Animator animator;

    public string Name { get { return "Door"; } }
    public bool IsActive { get; private set; }
    public InteractiveAction Action { get { return InteractiveAction.Use; } }

    public bool IsOpen { get; private set; }

    void Awake()
    {
        animator = GetComponent<Animator>();
        IsOpen = false;
        IsActive = isInteractive;
    }

    public void Use()
    {
        Open();
    }

    public void Open()
    {
        if (IsOpen == true) return;
        if (string.IsNullOrEmpty(keyId) || InventoryController.Instance.FindAndUseItem(keyId))
        {
            SoundController.Play("door", transform.position);
            animator.SetBool("IsOpen", true);
            IsActive = false;
            IsOpen = true;
        }
        else
        {
            UIController.Instance.ShowError("You need a key!");
        }
    }

    public void Close()
    {
        if (IsOpen == false) return;
        SoundController.Play("door", transform.position);
        animator.SetBool("IsOpen", false);
        IsOpen = false;
    }
}

