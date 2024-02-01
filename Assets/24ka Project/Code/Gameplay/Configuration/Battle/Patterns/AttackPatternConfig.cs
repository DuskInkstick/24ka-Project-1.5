using Code.Gameplay.Systems.Battle.Objects;
using Code.Gameplay.Systems.Battle.Patterns;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Configuration.Battle.Patterns
{
    internal abstract class AttackPatternConfig : ScriptableObject
    {
        [SerializeField] protected List<AttackingObject> AttackingObjects;

        [SerializeField] protected float ExecutionTime = 1f;
        [SerializeField] protected float AttackObjLifeTime = 10f;

        public abstract AttackPattern Create(Transform owner);
    }
}
