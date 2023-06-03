using Code.Gameplay.Player.Character.States;
using Code.Gameplay.State;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IStateSwitcher
{
    private CharacterInput _input;
    private Animator _animator;

    private List<StateBase> _states;
    private StateBase _currentState;

    private Vector2 _movementDirection;
    private Vector2 _viewDirection;
    private Vector2 _lastViewDirection;

    private bool _isFocused = false;

    public void SwitchState<T>() where T : StateBase
    {
        var newState = _states.Find(state => state is T);

        if (newState == null)
            throw new InvalidOperationException($"Character does not contain the state: {typeof(T).Name}");

        if(_currentState is IWatcher watcher) 
            _lastViewDirection = watcher.ViewDirection;

        _currentState.Stop();

        _currentState = newState;

        _currentState.Start();

        Look(_lastViewDirection);
        Focus(_isFocused);
    }

    private void Awake()
    {
        _input = GetComponent<CharacterInput>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _states = new List<StateBase>()
        {
            new IdleState(this, _animator),
            new WalkState(this, transform, 0.15f, _animator),
            new FocusState(this, transform, 0.07f, _animator)
        };
        _currentState = _states[0];
    }

    private void OnEnable()
    {
        _input.MovementVectorChanged += SetMovemetnDirection;
        _input.ViewVectorChanged += SetViewDirection;
        _input.FocusChanged += Focus;
    }

    private void FixedUpdate()
    {
        Move(_movementDirection);
        Look(_viewDirection);
    }

    private void OnDisable()
    {
        _input.MovementVectorChanged -= SetMovemetnDirection;
        _input.ViewVectorChanged -= SetViewDirection;
        _input.FocusChanged -= Focus;
    }

    private void SetMovemetnDirection(Vector2 direction)
    {
        _movementDirection = direction;
    }

    private void SetViewDirection(Vector2 direction)
    {
        _viewDirection = direction;
    }

    private void Move(Vector2 direction)
    {
        if(_currentState is IMovable movable)
            movable.Move(direction);
    }

    private void Look(Vector2 direction)
    {
        if (_currentState is IWatcher watcher)
            watcher.Look(_viewDirection);
    }

    private void Focus(bool isFocused)
    {
        _isFocused = isFocused;
        if (_currentState is IFocusable focusable)
            focusable.Focus(isFocused);
    }
}
