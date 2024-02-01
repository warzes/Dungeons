using UnityEngine;

public class DialogObject : MonoBehaviour, IInteractive
{
    public string objectName;
    public string dialogId;

    public string Name { get { return objectName; } }
    public bool IsActive { get { return true; } }
    public InteractiveAction Action { get { return InteractiveAction.Read; } }

    public void Use()
    {
        Dialog dialog = DialogsLibrary.Instance.GetDialogById(dialogId);
        if (dialog != null)
        {
            GameController.Instance.GameState = GameStates.Dialog;
            UIController.Instance.dialogScreen.SetDialog(dialog);
        }
    }
}
