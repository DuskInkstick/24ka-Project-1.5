using Player;
using Player.CharacterStates;
using Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    private CharacterInput _input;

    private List<StateBase> _states;
    private StateBase _currentState;

    private Vector2 _movementDirection;

    private void Awake()
    {
        _input = GetComponent<CharacterInput>();
    }

    private void Start()
    {
        var movement = new Movement(transform, 0.1f);

        _states = new List<StateBase>()
        {
            new WalkState(movement)
        };
        _currentState = _states[0];
    }

    private void OnEnable()
    {
        _input.OnMovementVectorChanged += SetMovemetnDirection;
    }

    private void FixedUpdate()
    {
        if (_movementDirection != Vector2.zero && _currentState is IMovable movable)
        {
            movable.Move(_movementDirection);
        }
    }

    private void OnDisable()
    {
        _input.OnMovementVectorChanged -= SetMovemetnDirection;
    }

    private void SetMovemetnDirection(Vector2 movementDirection)
    {
        _movementDirection = movementDirection;
    }
}
