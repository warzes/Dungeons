using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : Form
{
    public Button continueButton;
    public Button restartButton;
    public Button exitButton;

    void Start()
    {
        continueButton.onClick.AddListener(OnClickContinue);
        restartButton.onClick.AddListener(OnClickRestart);
        exitButton.onClick.AddListener(OnClickExit);
    }

    private void OnClickContinue()
    {
        GameController.Instance.GameState = GameStates.Game;
    }

    private void OnClickRestart()
    {
        GameController.Instance.RestartGame();
    }

    private void OnClickExit()
    {
        Application.Quit();
    }
}

