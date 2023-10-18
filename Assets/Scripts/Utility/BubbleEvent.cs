using UnityEngine;
using UnityEngine.Events;

public class BubbleEvent : MonoBehaviour
{
    public event UnityAction OnBubbleStartAttack = () => { };
    public event UnityAction OnBubbleComleteAttack = () => { };
    public event UnityAction OnBubbleHit = () => { };
    public event UnityAction OnBubbleCompleteDefeat = () => { };

    private void OnStartAttack()
    {
        OnBubbleStartAttack.Invoke();
    }

    private void OnCompleteAttack()
    {
        OnBubbleComleteAttack.Invoke();
    }

    private void OnHit()
    {
        OnBubbleHit.Invoke();
    }

    private void OnCompleteDefeat()
    {
        OnBubbleCompleteDefeat.Invoke();
    }
}
