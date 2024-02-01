using Code.Gameplay.Systems.Battle.Patterns;
using Code.Gameplay.Systems.Battle.Enums;
using UnityEngine;

namespace Code.Gameplay.Configuration.Battle.Patterns
{
    [CreateAssetMenu(
    fileName = "attack_pattern",
    menuName = "Scriptable Objects/Circular Attack Patter Config",
    order = 51)]
    internal class CircularAttackPatterConfig : AttackPatternConfig
    {
        [SerializeField] private float _timeBetweenAttacks = 0.5f;
        [SerializeField] private float _attackAngel = 360f;
        [SerializeField] private float _rotation = 0f;
        [SerializeField] private bool _considerTargetPosition = true;
        [SerializeField] private float _innerRadius = 0f;

        [Header("AttackingObj options")]
        [SerializeField] private int _attackObjCount = 4;

        [SerializeField] private SelectionMode _selectionMode = SelectionMode.Loop;
        [SerializeField] private bool _changeAttackObjInSameAttack = true;

        [SerializeField] private bool _rotateAttackingObj = false;
        [SerializeField] private float _attackObjInitSpeed = 1f;
        [SerializeField] private float _attackObjInitRotation = 0f;

        public override AttackPattern Create(Transform owner)
        {
            var pattern = new CircularAttackPattern(owner, AttackingObjects);

            pattern.ExecutionTime = ExecutionTime;
            pattern.AttackObjLifeTime = AttackObjLifeTime;

            pattern.TimeBetweenAttacks = _timeBetweenAttacks;
            pattern.AttackAngel = _attackAngel;
            pattern.ConsiderTargetPosition = _considerTargetPosition; 
            pattern.Rotation = _rotation;
            pattern.InnerRadius = _innerRadius;

            pattern.AttackObjCount = _attackObjCount;
            pattern.AttackObjSelectionMode = _selectionMode;
            pattern.ChangeAttackObjInSameAttack = _changeAttackObjInSameAttack;
            
            pattern.RotateAttackObj = _rotateAttackingObj;
            pattern.AttackObjInitSpeed = _attackObjInitSpeed;
            pattern.AttackObjInitRotation = _attackObjInitRotation;
            return pattern;
        }
    }
}
