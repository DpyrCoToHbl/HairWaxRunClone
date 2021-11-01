using UnityEngine;
using UnityEngine.Events;

public class HairinessStatesSwitcher : MonoBehaviour
{
    [SerializeField] private BodyHairCounter _bodyHairCounter;

    public string CurrentState { get; private set; }
    public string PreviousState {get; private set;}

    public event UnityAction StateChanged;
    private void Start()
    {
        CurrentState = GetState();
        StateChanged?.Invoke();
    }

    private void OnEnable()
    {
        _bodyHairCounter.HairCountChanged += OnHairCountChanged;
    }

    private void OnDisable()
    {
        _bodyHairCounter.HairCountChanged -= OnHairCountChanged;
    }

    private void OnHairCountChanged()
    {
        CurrentState = GetState();
        StateChanged?.Invoke();
    }

    private string GetState()
    {
        string state = null;
        int thinStateMinTreshold = 15;
        int thinStateMaxTreshold = 19;
        int hairlessStateMinTreshold = 10;
        int hairlessStateMaxTreshold = 14;
        int smoothStateMinTreshold = 0;
        int smoothStateMaxTreshold = 9;

        PreviousState = CurrentState;

        if (_bodyHairCounter.HairCount >= 20)
            state = HairinessStatesHolder.Hairy;

        if (_bodyHairCounter.HairCount == Mathf.Clamp(_bodyHairCounter.HairCount, thinStateMinTreshold, thinStateMaxTreshold))
            state = HairinessStatesHolder.Thin;

        if (_bodyHairCounter.HairCount == Mathf.Clamp(_bodyHairCounter.HairCount, hairlessStateMinTreshold, hairlessStateMaxTreshold))
            state = HairinessStatesHolder.Hairless;

        if (_bodyHairCounter.HairCount == Mathf.Clamp(_bodyHairCounter.HairCount, smoothStateMinTreshold, smoothStateMaxTreshold))
            state = HairinessStatesHolder.Smooth;

        return state;
    }
}
