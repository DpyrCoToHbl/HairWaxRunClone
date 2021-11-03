using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BodyHairCounter))]
public class Player : MonoBehaviour
{
    private BodyHairCounter _bodyHairCounter;

    public bool IsFinished { get; private set; }
    public bool IsOnDuctTape { get; private set; }

    public event UnityAction FinishLineReached;
    public event UnityAction NegativeItemCollected;
    public event UnityAction PositiveItemCollected;
    public event UnityAction SteppedOnRedDuctTape;
    public event UnityAction SteppedOnGreenDuctTape;
    public event UnityAction GotOffDuctTape;
    public event UnityAction JumpPadReached;
    public event UnityAction Lose;

    private void Start()
    {
        _bodyHairCounter = GetComponent<BodyHairCounter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NegativeObject negativeObject))
            NegativeItemCollected?.Invoke();

        if (other.TryGetComponent(out PositiveObject positiveObject))
            PositiveItemCollected?.Invoke();

        if(other.gameObject.TryGetComponent(out DuctTape ductTape))
        {
            if (other.gameObject.name == "Red")
            {
                Debug.Log(other.gameObject.name);
                SteppedOnRedDuctTape?.Invoke();
                SwitchOnDuckTapeFlag();
            }

            if (other.gameObject.name == "Green")
            {
                Debug.Log(other.gameObject.name);

                if (_bodyHairCounter.HairCount != 0)
                {
                    SteppedOnGreenDuctTape?.Invoke();
                    SwitchOnDuckTapeFlag();
                }
            }
        }

        if (other.TryGetComponent(out FinishLine finishLine))
        {
            FinishLineReached?.Invoke();
            IsFinished = true;
        }

        if (other.TryGetComponent(out JumpPad jumpPad))
            JumpPadReached?.Invoke();

        if (other.TryGetComponent(out DuctTapeHair ductTapeHair))
            ductTapeHair.ToggleVisibility();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out RedDuctTape redDuctTape))
        {
            GotOffDuctTape?.Invoke();
            SwitchOnDuckTapeFlag();
        }

        if (other.TryGetComponent(out GreenDuctTape greenDuctTape))
        {
            if (_bodyHairCounter.HairCount != 0)
            {
                GotOffDuctTape?.Invoke();
                SwitchOnDuckTapeFlag();
            }
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
