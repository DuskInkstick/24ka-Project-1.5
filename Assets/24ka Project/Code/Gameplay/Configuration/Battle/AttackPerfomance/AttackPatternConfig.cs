using Code.Gameplay.Systems.Battle.AttackingObjects;
using Code.Gameplay.Systems.Battle.AttackPerfomance;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Configuration.Battle.AttackPerfomance
{
    internal abstract class AttackPatternConfig : ScriptableObject
    {
        [SerializeField] protected List<AttackingObject> AttackingObjects;

        public abstract AttackPattern Get(Transform spawnPoint);
    }
}
