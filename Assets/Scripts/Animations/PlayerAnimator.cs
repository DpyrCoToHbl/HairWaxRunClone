using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private HairinessStatesSwitcher _hairinessStatesSwitcher;
    
    private Player _player;
    private Animator _animator;
    private string _currentAnimationName;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _playerMover.Moving += OnMoving;
        _playerMover.Grounded += OnGrounded;
        _playerMover.Floating += OnFloating;
        _player.FinishLineReached += OnFinishLineReached;
        _player.Lose += OnLoose;
        _player.SteppedOnGreenDuctTape += OnSteppedOnGreenDuctTape;
        _player.SteppedOnRedDuctTape += OnSteppedOnRedDuctTape;
    }

    private void OnDisable()
    {
        _playerMover.Moving -= OnMoving;
        _playerMover.Grounded -= OnGrounded;
        _playerMover.Floating -= OnFloating;
        _player.FinishLineReached -= OnFinishLineReached;
        _hairinessStatesSwitcher.StateChanged -= OnStateChanged;
        _player.Lose -= OnLoose;
        _player.SteppedOnGreenDuctTape -= OnSteppedOnGreenDuctTape;
        _player.SteppedOnRedDuctTape -= OnSteppedOnRedDuctTape;
    }

    private void OnMoving()
    {
        _animator.SetBool(AnimationStatesHolder.SadWalkAnimation, true);
        _currentAnimationName = AnimationStatesHolder.SadWalkAnimation;
        _hairinessStatesSwitcher.StateChanged += OnStateChanged;
    }

    private void OnGrounded()
    {
        _animator.SetTrigger(AnimationStatesHolder.LandingAnimation);
    }

    private void OnFloating()
    {
        _animator.Play(AnimationStatesHolder.FloatingAnimation);
    }

    private void OnFinishLineReached()
    {
        _animator.Play(AnimationStatesHolder.DancingAnimation);
    }

    private void OnLoose()
    {
        _animator.Play(AnimationStatesHolder.DeathAnimation);
    }

    private void OnSteppedOnGreenDuctTape()
    {
        _animator.Play(AnimationStatesHolder.LieDownAnimation);
    }

    private void OnSteppedOnRedDuctTape()
    {
        _animator.Play(AnimationStatesHolder.LieDownAnimation);
    }

    private void OnStateChanged()
    {
        if (_hairinessStatesSwitcher.CurrentState == HairinessStatesHolder.Hairy)
            TryGetAnimattion(AnimationStatesHolder.SadWalkAnimation);

        if (_hairinessStatesSwitcher.CurrentState == HairinessStatesHolder.Thin)
            TryGetAnimattion(AnimationStatesHolder.FemaleWalkAnimation);

        if (_hairinessStatesSwitcher.CurrentState == HairinessStatesHolder.Hairless)
            TryGetAnimattion(AnimationStatesHolder.CatWalkAnimation);

        if (_hairinessStatesSwitcher.CurrentState == HairinessStatesHolder.Smooth)
            TryGetAnimattion(AnimationStatesHolder.GoofyRunnindAnimation);
    }

    private void TryGetAnimattion(string animation)
    {
        if (animation != _currentAnimationName)
        {
            _animator.SetBool(_currentAnimationName, false);
            _animator.SetTrigger(AnimationStatesHolder.SpinAnimation);
            _currentAnimationName = animation;
            _animator.SetBool(animation, true);
        }
    }

    
}
