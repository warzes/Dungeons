using UnityEngine;

public class UIController : Singleton<UIController>
{
    public PauseScreen pauseScreen;
    public GameScreen gameScreen;
    public FinishScreen finishScreen;
    public DialogScreen dialogScreen;
    public InventoryScreen InventoryScreen;

    void Awake()
    {
        GameController.Instance.onStateChanged += OnGameStateChanged;
    }

    void OnGameStateChanged(GameStates state)
    {
        if (state == GameStates.Game)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        pauseScreen.SetState(state == GameStates.Pause);
        gameScreen.SetState(state == GameStates.Game || state == GameStates.Inventory);
        finishScreen.SetState(state == GameStates.Finish);
        dialogScreen.SetState(state == GameStates.Dialog);
        InventoryScreen.SetState(state == GameStates.Inventory);
    }

    public void ShowError(string error)
    {
        SoundController.Play("error");
        gameScreen.ShowError(error);
    }
}