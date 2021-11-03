using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HairnessBarDisplay : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private TMP_Text _animatedText;
    [SerializeField] private BodyHairCounter _bodyHairCounter;
    [SerializeField] private HairinessStatesSwitcher _hairinessStatesSwitcher;

    private Animator _animator;
    private string _textAnimationName = "Appear";

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        ChangeVizualization();
        _animatedText.gameObject.SetActive(false);
        _slider.maxValue = _bodyHairCounter.MaxHairQuantity;
        _slider.value = _bodyHairCounter.HairCount;
    }

    private void OnEnable()
    {
        _bodyHairCounter.HairCountChanged += OnHairCountChanged;
        _hairinessStatesSwitcher.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _bodyHairCounter.HairCountChanged -= OnHairCountChanged;
        _hairinessStatesSwitcher.StateChanged -= OnStateChanged;
    }

    private void OnHairCountChanged()
    {
        _slider.value = _bodyHairCounter.HairCount;
    }

    private void OnStateChanged()
    {
        ChangeVizualization();
        TryPlayAnimation();
    }

    private void ChangeVizualization()
    {
        Color32 HairyStateColor = new Color32(250, 142, 4, 255);
        Color32 ThinStateColor = new Color32(178, 26, 238, 255);
        Color32 HairlessStateColor = new Color32(48, 233, 14, 255);
        Color32 SmoothStateColor = new Color32(28, 153, 255, 255);

        if (_hairinessStatesSwitcher.CurrentState == HairinessStatesHolder.Hairy)
        {
            GetParams(HairinessStatesHolder.Hairy, HairyStateColor);
        }

        if (_hairinessStatesSwitcher.CurrentState == HairinessStatesHolder.Thin)
        {
            GetParams(HairinessStatesHolder.Thin, ThinStateColor);
        }

        if (_hairinessStatesSwitcher.CurrentState == HairinessStatesHolder.Hairless)
        {
            GetParams(HairinessStatesHolder.Hairless, HairlessStateColor);
        }

        if (_hairinessStatesSwitcher.CurrentState == HairinessStatesHolder.Smooth)
        {
            GetParams(HairinessStatesHolder.Smooth, SmoothStateColor);
        }
    }

    private void GetParams(string stateName, Color32 color)
    {
        var fill = _slider.GetComponentsInChildren<Image>().FirstOrDefault(fillerImage => fillerImage.name == "Fill");

        _text.text = stateName;
        _text.color = color;
        fill.color = color;
        _animatedText.text = stateName;
        _animatedText.color = color;
    }

    private void TryPlayAnimation()
    {
        if (_hairinessStatesSwitcher.PreviousState != _hairinessStatesSwitcher.CurrentState)
        {
            _animatedText.gameObject.SetActive(true);
            _animator.SetTrigger(_textAnimationName);
        }

    }
}
