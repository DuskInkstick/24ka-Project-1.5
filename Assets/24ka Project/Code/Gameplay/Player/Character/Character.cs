using Assets._24ka_Project.Code.Gameplay.State;
using Code.Gameplay.Creatures;
using Code.Gameplay.Player.Abilities.IceWall;
using Code.Gameplay.Player.Weapons;
using Code.Gameplay.State;
using Code.Gameplay.Systems.Animation;
using Code.Gameplay.Systems.Movements;
using Code.Utils;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._24ka_Project.Code.Gameplay.Player.Character
{
    [RequireComponent(typeof(CharacterInput))]
    [RequireComponent(typeof(Rigidbody2D))]
    internal class Character : CreatureBase
    {
        [SerializeField] private Staff _staff;
        [SerializeField] private IceWall _horizontalWall;
        [SerializeField] private IceWall _verticalWall;

        private CharacterInput _input;
        private Animator _animator;

        private Vector2 _moveVector;
        private Vector2 _lookVector;

        private bool _isFocused;

        private float _pressExtraTime = 0.25f;

        private float _jumpPressExtraTimer = 0;
        private float _defensePressExtraTimer = 0;

        public void Initialize()
        {
            InitializeStates();
            AddTransitions();
        }

        protected override void Start()
        {
            base.Start();
            Initialize();
        }

        protected override void Update()
        {
            base.Update();

            CurrentState.Update(
                _lookVector,
                _moveVector,
                (Vector2)transform.position + CurrentState.ViewVector);

            if (_jumpPressExtraTimer > 0f)
                _jumpPressExtraTimer -= Time.deltaTime;

            if( _defensePressExtraTimer > 0f)
                _defensePressExtraTimer -= Time.deltaTime;

            if(CurrentState is AttackState)
                _staff.PlayAttack();
        }

        private void Awake()
        {
            _input = GetComponent<CharacterInput>();
            _animator = GetComponent<Animator>();

            _staff.Owner = transform;
            _staff.AllyGroup = AllyGroup;
        }

        private void OnEnable()
        {
            _input.MovementVectorChanged += SetMovemetnVector;
            _input.LookVectorChanged += SetLookVector;
            _input.FocusChanged += Focus;
            _input.Jumped += Jump;
            _input.ActivatedDefence += Defense;
        }

        private void OnDisable()
        {
            _input.MovementVectorChanged -= SetMovemetnVector;
            _input.LookVectorChanged -= SetLookVector;
            _input.FocusChanged -= Focus;
            _input.Jumped -= Jump;
            _input.ActivatedDefence -= Defense;
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
        }

        private void Jump()
        {
            _jumpPressExtraTimer = _pressExtraTime;
        }

        private void Defense()
        {
            _defensePressExtraTimer = _pressExtraTime;
        }

        private void InitializeStates()
        {
            var movement = new Movement(transform, 8);
            var focusMovement = new Movement(transform, 4);

            States = new List<CreatureStateBase>()
        {
            new IdleState(
                this,
                new FourSideAnimation(_animator, "idle_up", "idle_down", "idle_left", "idle_right")),
            new MoveState(
                this,
                new FourSideAnimation(_animator, "walk_up", "walk_down", "walk_left", "walk_right"),
                movement) { Phase = 0},
            new MoveState(
                this,
                new FourSideAnimation(_animator, "walk_up", "walk_down", "walk_left", "walk_right"),
                focusMovement) { Phase = 1 },
            new JumpState(
                this,
                new FourSideAnimation(_animator, "dash_up", "dash_down", "dash_left", "dash_right"),
                new Movement(transform, 12)),
            new AttackState(
                this,
                new FourSideAnimation(_animator, "walk_up", "walk_down", "walk_left", "walk_right"),
                _staff.AttackSeries,
                movement) { Phase = 0},
            new AttackState(
                this,
                new FourSideAnimation(_animator, "walk_up", "walk_down", "walk_left", "walk_right"),
                _staff.AttackSeries,
                focusMovement) { Phase = 1},
            new DefenseState(
                this,
                new FourSideAnimation(_animator, "dash_up", "dash_down", "dash_left", "dash_right"),
                new IcePlacer(_horizontalWall, _verticalWall)) { ExecutionTime = 0f },
            new DeathState(
                this,
                new FourSideAnimation(_animator, "idle_up", "idle_down", "idle_left", "idle_right"))
        };
            CurrentState = States[0];
        }

        private void AddTransitions()
        {
            foreach (var state in States)
            {
                switch (state)
                {
                    case IdleState idle:
                        idle.SetTransition(IdleStateTransitions);
                        break;
                    case MoveState move:
                        move.SetTransition(MoveStateTransitions);
                        break;
                    case JumpState jump:
                        jump.SetTransition(UninterruptedStatesTransitions);
                        break;
                    case AttackState attack:
                        attack.SetTransition(AttackStateTransitions);
                        break;
                    case DefenseState defense:
                        defense.SetTransition(UninterruptedStatesTransitions);
                        break;
                }
            }
        }

   
        private void IdleStateTransitions(bool isComplited)
        {
            if(TrySwithToJumpState()) return;

            if(TrySwithToDefenseState()) return;

            if(TrySwithToAttackState()) return;

            TrySwithToMoveState();
        }

        private void MoveStateTransitions(bool isComplited)
        {
            if(TrySwithToJumpState()) return;

            if(TrySwithToDefenseState()) return;

            if(TrySwithToAttackState()) return;

            if(_moveVector.ToMoveDirection() == MoveDirection.None)
            {
                SwitchState<IdleState>();
                return;
            }

            if(CurrentState.Phase == 0 && _isFocused)
                SwitchState<MoveState>(1);

            else if(CurrentState.Phase == 1 && _isFocused == false)
                SwitchState<MoveState>(0);
        }

        private void AttackStateTransitions(bool isComplited)
        {
            if(isComplited == false)
                return;

            if (TrySwithToJumpState()) return;

            if (TrySwithToDefenseState()) return;

            if(_lookVector.ToMoveDirection() == MoveDirection.None)
            {
                if (TrySwithToMoveState()) return;

                SwitchState<IdleState>();
                return;
            }

            if (CurrentState.Phase == 0 && _isFocused)
            {
                SwitchState<AttackState>(1);
                return;
            }
            else if (CurrentState.Phase == 1 && _isFocused == false)
            {
                SwitchState<AttackState>(0);
                return;
            }           
        }

        private void UninterruptedStatesTransitions(bool isComplited)
        {
            if (isComplited == false)
                return;

            if (TrySwithToJumpState()) return;

            if (TrySwithToDefenseState()) return;

            if (TrySwithToAttackState()) return;

            if (TrySwithToMoveState()) return;

            SwitchState<IdleState>();
        }

        private bool TrySwithToMoveState()
        {
            if (_moveVector.ToMoveDirection() == MoveDirection.None)
                return false;

            if (_isFocused)
            {
                SwitchState<MoveState>(1);
                return true;
            }

            SwitchState<MoveState>(0);
            return true;
        }

        private bool TrySwithToJumpState()
        {
            if (_jumpPressExtraTimer <= 0f)
                return false;

            _jumpPressExtraTimer = 0f;

            SwitchState<JumpState>();
            return true;
        }

        private bool TrySwithToAttackState()
        {
            if (_lookVector.ToMoveDirection() == MoveDirection.None)
                return false;

            if (_isFocused)
            {
                SwitchState<AttackState>(1);
                return true;
            }

            SwitchState<AttackState>(0);
            return true;
        }

        private bool TrySwithToDefenseState()
        {
            if (_defensePressExtraTimer <= 0f)
                return false;

            _defensePressExtraTimer = 0f;

            SwitchState<DefenseState>();
            return true;
        }
    }
}
