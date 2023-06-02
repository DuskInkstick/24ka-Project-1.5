using Code.Gameplay.Player.Character.States;
using Code.Gameplay.State;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IStateSwither
{
    private CharacterInput _input;
    private Animator _animator;

    private List<StateBase> _states;
    private StateBase _currentState;

    private Vector2 _movementDirection;

    public void SwithState<T>() where T : StateBase
    {
        var newState = _states.Find(state => state is T);

        if (newState == null)
            throw new InvalidOperationException($"Character does not contain the state: {typeof(T).Name}");

        _currentState.Stop();
        _currentState = newState;
        _currentState.Start();
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
            new WalkState(this, transform, 0.15f, _animator),
            new FocusState(this, transform, 0.07f, _animator)
        };
        _currentState = _states[0];
    }

    private void OnEnable()
    {
        _input.OnMovementVectorChanged += SetMovemetnDirection;
        _input.OnFocusChanged += SetFocusState;
    }

    private void FixedUpdate()
    {
        if (_currentState is IMovable movable)
        {
            movable.Move(_movementDirection);
        }
    }

    private void OnDisable()
    {
        _input.OnMovementVectorChanged -= SetMovemetnDirection;
        _input.OnFocusChanged -= SetFocusState;
    }

    private void SetMovemetnDirection(Vector2 movementDirection)
    {
        _movementDirection = movementDirection;
    }

    private void SetFocusState(bool isFocused)
    {
        if (_currentState is IFocusable focusable)
            focusable.Focus(isFocused);
    }
}
