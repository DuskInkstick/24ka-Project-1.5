using Code.Gameplay.Systems.Battle.AttackPerfomance;
using Code.Gameplay.Systems.Battle.Enums;
using UnityEngine;

namespace Code.Gameplay.Configuration.Battle.AttackPerfomance
{
    [CreateAssetMenu(
    fileName = "attack_pattern",
    menuName = "Scriptable Objects/Circular Attack Patter Config",
    order = 51)]
    internal class CircularAttackPatterConfig : AttackPatternConfig
    {
        [SerializeField] private int _count = 4;
        [SerializeField] private float _attackAngel = 360f;
        [SerializeField] private float _rotation = 0f;
        [SerializeField] private bool _freezeRotation = false;
        [SerializeField] private float _radius = 5f;
        [SerializeField] private float _innerRadius = 0f;

        [SerializeField] private LoopMode _loopMode = LoopMode.Loop;
        [SerializeField] private bool _differentInSameAttack = false;

        [SerializeField] private bool _rotateAttackingObjects = false;
        [SerializeField] private float _initSpeed = 1f;

        public override AttackPattern Get(Transform spawnPoint)
        {
            var pattern = new CircularAttackPattern(spawnPoint, AttackingObjects);

            pattern.Count = _count;
            pattern.AttackAngel = _attackAngel;
            pattern.FreezeRotaion = _freezeRotation; 
            pattern.Rotation = _rotation;
            pattern.Radius = _radius;
            pattern.InnerRadius = _innerRadius;

            pattern.LoopMode = _loopMode;
            pattern.DifferentInSameAttack = _differentInSameAttack;
            
            pattern.RotateAttackingObjects = _rotateAttackingObjects;
            pattern.InitSpeed = _initSpeed;
            return pattern;
        }
    }
}
