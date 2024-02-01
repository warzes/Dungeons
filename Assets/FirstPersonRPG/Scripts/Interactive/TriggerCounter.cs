using UnityEngine;
using UnityEngine.Events;

public class TriggerCounter : MonoBehaviour
{
    public int currentValue = 0;
    public int targetValue = 5;

    public UnityEvent action;

    public void Increment()
    {
        currentValue++;
        if (currentValue == targetValue) action.Invoke();
    }

    public void Decrement()
    {
        currentValue++;
        if (currentValue == targetValue) action.Invoke();
    }
}
