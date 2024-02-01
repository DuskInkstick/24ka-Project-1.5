using Code.Gameplay.Creatures;
using Code.Gameplay.Player.Abilities.IceWall;
using Code.Gameplay.Player.Weapons;
using Code.Gameplay.State;
using Code.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterInput))]
[RequireComponent(typeof(Rigidbody2D))]
public class CharacterOld : CreatureBase
{
   /* [SerializeField] private Staff _staff;
    [SerializeField] private IceWall _horizontalWall;
    [SerializeField] private IceWall _verticalWall;

    private CharacterInput _input;

    private Vector2 _moveVector;
    private Vector2 _lookVector;

    private bool _isFocused = false;

    public override bool SwitchState<T>(int tag = 0)
    {
        if(base.SwitchState<T>(tag) == false)
            return false;

        Focus(_isFocused);
        return true;
    }

    public override void InitializeStates()
    {
        var icePlacer = new IcePlacer(transform, _horizontalWall, _verticalWall);

        States = new List<CreatureStateBase>()
        {
            new QuiescentState(this, Animator, _staff.AttackBehavior, icePlacer),
            new WalkState(this, Animator, transform, 7f, _staff.AttackBehavior),
            new FocusState(this, Animator, transform, 3f, _staff.AttackBehavior, icePlacer),
            new DashState(this, Animator, transform, 12f)
        };
        CurrentState = States[0];
    }

    protected override void Awake()
    {
        base.Awake();
        _input = GetComponent<CharacterInput>();

        _staff.Owner = transform;
        _staff.AllyGroup = AllyGroup;
    }

    protected override void OnDead(int deadDamage) { }

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
        CurrentState.MoveIn(_moveVector);
        CurrentState.LookIn(_lookVector);

        if (_lookVector.ToMoveDirection() != MoveDirection.None)
            CurrentState.Attack(true);

        CurrentState.Update();
    }

    private void OnDisable()
    {
        _input.MovementVectorChanged -= SetMovemetnVector;
        _input.LookVectorChanged -= SetLookVector;
        _input.FocusChanged -= Focus;
        _input.DashOrBlock -= Escape;
        _input.LongBlock -= PerformLongBlock;
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

        if (CurrentState is CharacterActionState character)
            character.Focus(isFocused);
    }

    private void Escape()
    {
        if (CurrentState is CharacterActionState character)
            character.UseEscapeSkill();
    }

    private void PerformLongBlock(bool performing)
    {
        Debug.Log("LongBlock " + performing);
    }*/
}
