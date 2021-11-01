using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public bool IsFinished { get; private set; }

    public event UnityAction FinishLineReached;
    public event UnityAction NegativeItemCollected;
    public event UnityAction PositiveItemCollected;
    public event UnityAction SteppedOnRedDuctTape;
    public event UnityAction SteppedOnGreenDuctTape;
    public event UnityAction GotOffDuctTape;
    public event UnityAction JumpPadReached;
    public event UnityAction Lose;

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out NegativeObject negativeObject))
            NegativeItemCollected?.Invoke();

        if (other.TryGetComponent(out PositiveObject positiveObject))
            PositiveItemCollected?.Invoke();

        if (other.TryGetComponent(out RedDuctTape redDuctTape))
            SteppedOnRedDuctTape?.Invoke();

        if (other.TryGetComponent(out GreenDuctTape greenDuctTape))
            SteppedOnGreenDuctTape?.Invoke();

        if (other.TryGetComponent(out FinishLine finishLine))
        {
            FinishLineReached?.Invoke();
            IsFinished = true;
        }

        if (other.TryGetComponent(out JumpPad jumpPad))
            JumpPadReached?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out RedDuctTape redDuctTape))
            GotOffDuctTape?.Invoke();

        if (other.TryGetComponent(out GreenDuctTape greenDuctTape))
            GotOffDuctTape?.Invoke();
    }

    public void Die()
    {
        Lose?.Invoke();
    }
}
