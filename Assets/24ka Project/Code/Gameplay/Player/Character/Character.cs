using Code.Gameplay.Player.Abilities.IceWall;
using Code.Gameplay.Player.Character.States;
using Code.Gameplay.Player.Weapons;
using Code.Gameplay.State;
using Code.Gameplay.Systems.Battle;
using Code.Gameplay.Systems.Battle.Elementals;
using Code.Gameplay.Systems.Battle.Enums;
using Code.Interfaces.Architecture;
using Code.Interfaces.Gameplay;
using Code.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour, IStateSwitcher, IDamageable
{
    [SerializeField] private Staff _staff;
    [SerializeField] private IceWall _horizontalWall;
    [SerializeField] private IceWall _verticalWall;

    private CharacterInput _input;
    private Animator _animator;

    private List<CreatureStateBase> _states;
    private CreatureStateBase _currentState;

    private Resilience _resilience;

    private Vector2 _moveVector;
    private Vector2 _lookVector;

    private bool _isFocused = false;

    public int AllyGroup => 0;

    public void SwitchState<T>() where T : CreatureStateBase
    {
        var newState = _states.Find(state => state is T);

        if (newState == null)
            throw new InvalidOperationException($"Character does not contain the state: {typeof(T).Name}");
       
        var lastViewVector = _currentState.ViewVector;
        _currentState.Stop();

        _currentState = newState;
        _currentState.Start();

        _currentState.LookIn(lastViewVector);

        Focus(_isFocused);
    }

    public CausedDamage ApplyDamage(CausedDamage damage)
    {
        return _currentState.ApplyDamage(damage, _resilience);
    }

    private void Awake()
    {
        _input = GetComponent<CharacterInput>();
        _animator = GetComponent<Animator>();

        _staff.Owner = transform;
        _staff.AllyGroup = AllyGroup;
    }

    private void Start()
    {
        _resilience = new Resilience(5, new ElementalAttribute(ElementalAttributeType.None, 0));
        _resilience.Resistance.Add(ElementalAttributeType.Ice, 2);

        _resilience.StatusOverloaded += OnStatusOverloaded;
        _resilience.Dead += OnDead;

        var icePlacer = new IcePlacer(transform, _horizontalWall, _verticalWall);

        _states = new List<CreatureStateBase>()
        {
            new QuiescentState(this, _animator, _staff.AttackBehavior, icePlacer),
            new WalkState(this, _animator, transform, 7f, _staff.AttackBehavior),
            new FocusState(this, _animator, transform, 3f, _staff.AttackBehavior, icePlacer),
            new DashState(this, _animator, transform, 12f)
        };
        _currentState = _states[0];
    }

    private void OnEnable()
    {
        _input.MovementVectorChanged += SetMovemetnVector;
        _input.LookVectorChanged += SetLookVector;
        _input.FocusChanged += Focus;
        _input.DashOrBlock += Escape;
        _input.LongBlock += PerformLongBlock;
    }

    private void Update()
    {
        _currentState.MoveIn(_moveVector);
        _currentState.LookIn(_lookVector);

        if (_lookVector.ToMoveDirection() != MoveDirection.None)
            _currentState.Attack();

        _currentState.Update();
    }

    private void OnDisable()
    {
        _input.MovementVectorChanged -= SetMovemetnVector;
        _input.LookVectorChanged -= SetLookVector;
        _input.FocusChanged -= Focus;
        _input.DashOrBlock -= Escape;
        _input.LongBlock -= PerformLongBlock;
    }

    private void OnDestroy()
    {
        _resilience.StatusOverloaded -= OnStatusOverloaded;
        _resilience.Dead -= OnDead;
    }

    private void SetMovemetnVector(Vector2 direction)
    {
        _moveVector = direction;
    }

    private void SetLookVector(Vector2 direction)
    {
        _lookVector = direction;
    }

    private void Focus(bool isFocused)
    {
        _isFocused = isFocused;

        if (_currentState is CharacterActionState character)
            character.Focus(isFocused);
    }

    private void Escape()
    {
        if (_currentState is CharacterActionState character)
            character.UseEscapeSkill();
    }

    private void PerformLongBlock(bool performing)
    {
        Debug.Log("LongBlock " + performing);
    }

    private void OnStatusOverloaded(ElementalAttributeType status)
    {
        _currentState.OnStatusOverloaded(status);
    }

    private void OnDead(int deadDamage)
    {
        _currentState.OnDead(deadDamage);
    }
}
