using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private BodyHairCounter _bodyHairCounter;

    private const string RedDuctTapeName = "Red";
    private const string GreenDuctTapeName = "Green";

    public bool IsFinished { get; private set; }
    public bool IsOnDuctTape { get; private set; }

    public event UnityAction<GameObject, string> SteppedOnDuctTape;
    public event UnityAction FinishLineReached;
    public event UnityAction NegativeItemCollected;
    public event UnityAction PositiveItemCollected;
    public event UnityAction GotOffDuctTape;
    public event UnityAction JumpPadReached;
    public event UnityAction Lose;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out NegativeObject negativeObject))
            NegativeItemCollected?.Invoke();

        if (other.gameObject.TryGetComponent(out PositiveObject positiveObject))
            PositiveItemCollected?.Invoke();

        if (other.gameObject.TryGetComponent(out DuctTape ductTape))
        {
            if (other.gameObject.name == RedDuctTapeName)
            {
                SteppedOnDuctTape?.Invoke(other.gameObject, RedDuctTapeName);
                SwitchOnDuckTapeFlag();
            }

            if (other.gameObject.name == GreenDuctTapeName)
            {
                if (_bodyHairCounter.HairCount != 0)
                {
                    SteppedOnDuctTape?.Invoke(other.gameObject, GreenDuctTapeName);
                    SwitchOnDuckTapeFlag();
                }
            }
        }

        if (other.gameObject.TryGetComponent(out FinishLine finishLine))
        {
            FinishLineReached?.Invoke();
            IsFinished = true;
        }

        if (other.gameObject.TryGetComponent(out JumpPad jumpPad))
            JumpPadReached?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out DuctTape ductTape) && IsOnDuctTape)
        {
            GotOffDuctTape?.Invoke();
            SwitchOnDuckTapeFlag();
        }
    }

    private void SwitchOnDuckTapeFlag()
    {
        if (IsOnDuctTape)
            IsOnDuctTape = false;
        else
            IsOnDuctTape = true;
    }

    public void Die()
    {
        Lose?.Invoke();
    }
}
