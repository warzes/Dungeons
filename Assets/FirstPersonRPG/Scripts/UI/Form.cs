using UnityEngine;

public class Form : MonoBehaviour
{
    public GameObject rootPanel;

    public bool IsShown { get; private set; }

    public virtual void Show()
    {
        rootPanel.SetActive(true);
        IsShown = true;
    }

    public virtual void Hide()
    {
        rootPanel.SetActive(false);
        IsShown = false;
    }

    public void SetState(bool show)
    {
        if (show) Show();
        else Hide();
    }
}