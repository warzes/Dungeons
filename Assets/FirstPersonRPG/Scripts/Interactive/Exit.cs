using UnityEngine;

public class Exit : MonoBehaviour, IInteractive
{
    public string Name { get { return "Exit"; } }
    public bool IsActive { get; private set; }
    public InteractiveAction Action { get { return InteractiveAction.Use; } }

    void Start()
    {
        IsActive = true;
    }

    public void Use()
    {
        GameController.Instance.GameState = GameStates.Finish;
        IsActive = false;
    }
}
