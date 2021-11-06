using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HairinessStatesSwitcher))]
public class ClothesColorChanger : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;

    private HairinessStatesSwitcher _hairinessStatesSwitcher;
    private List<Color32> _colors = new List<Color32> { new Color32(183, 82, 47, 57), new Color32(128, 43, 128, 255), new Color32(34, 175, 85, 255), new Color32(50, 124, 189, 255) };

    private void Awake()
    {
        _hairinessStatesSwitcher = GetComponent<HairinessStatesSwitcher>();
    }

    private void SwitchColor(Color32 color)
    {
        foreach (var material in _materials)
        {
            material.color = color;
        }
    }

    private void OnEnable()
    {
        _hairinessStatesSwitcher.StateChanged += OnStateChanged;
    }

    private void OnDisable()
    {
        _hairinessStatesSwitcher.StateChanged += OnStateChanged;
    }

    private void OnStateChanged()
    {
        if (_hairinessStatesSwitcher.CurrentState == HairinessStatesHolder.Hairy)
            SwitchColor(_colors[0]);

        if (_hairinessStatesSwitcher.CurrentState == HairinessStatesHolder.Thin)
            SwitchColor(_colors[1]);

        if (_hairinessStatesSwitcher.CurrentState == HairinessStatesHolder.Hairless)
            SwitchColor(_colors[2]);

        if (_hairinessStatesSwitcher.CurrentState == HairinessStatesHolder.Smooth)
            SwitchColor(_colors[3]);
    }
}
