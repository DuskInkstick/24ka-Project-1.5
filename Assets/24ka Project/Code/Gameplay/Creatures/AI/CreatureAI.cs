using Code.Gameplay.Systems.Movements;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Code.Utils;

namespace Code.Gameplay.Creatures.AI
{
    internal abstract class CreatureAI
    {
        private readonly Collider2D _self;
        private readonly List<Collider2D> _observableCreatures;
        private readonly List<(float distance, Collider2D other)> _othersInSight;

        private const float _checkOthersTime = 0.5f;
        private float _checkOthersTimer = Random.Range(0f, _checkOthersTime);

        private readonly DiscreteDirections _directions;

        public bool Enable { get; set; } = true;
        public float ViewingAngle { get; set; } = 360f;
        public float ViewingDistance { get; set; } = 10f;

        public Vector2 MoveVector { get; private set; }
        public Vector2 LookVector { get; private set; } = Vector2.down;
        public bool IsAttacking { get; private set; }

        protected Vector2 Position => _self.bounds.center;

        protected CreatureAI(Collider2D self,
                             IEnumerable<Collider2D> observableCreatures,
                             DiscreteDirections directions = null)
        {
            _self = self;

            _observableCreatures = observableCreatures == null ?
                new List<Collider2D>() : observableCreatures.ToList();

            _directions = directions;

            _othersInSight = new List<(float, Collider2D)>();
        }

        public void Update()
        {
            if(Enable == false) 
                return;

            CheckOthersInSight();

            LookVector = CalculateLook();

            MoveVector = CalculateMovement();

            IsAttacking = CalculateAttack();
        }

        protected abstract Vector2 CalculateMovement();

        protected abstract Vector2 CalculateLook();

        protected abstract bool CalculateAttack();

        protected abstract void ProcessOthersInSight(IList<(float, Collider2D)> others);

        protected Vector2 GetVectorTo(Vector2 destination)
        {
            if(_directions == null)
                return (destination - Position).normalized;

            return _directions.GetVector(Position, destination);
        }

        protected Vector2 GetRandomVector()
        {
            return _directions.GetVector(Vector2.zero, Vector2Helper.GetRandom());
        }

        private void CheckOthersInSight()
        {
            if(_observableCreatures.Count == 0) 
                return;
            
            _checkOthersTimer += Time.deltaTime;

            if (_checkOthersTimer < _checkOthersTime)
                return;

            _checkOthersTimer = 0f;

            _othersInSight.Clear();

            Physics2D.queriesHitTriggers = false;

            for (int i = 0; i < _observableCreatures.Count; i++)
            {
                var distance = IsOtherVisible(i);

                if(distance > -1)
                    _othersInSight.Add((distance, _observableCreatures[i]));
            }

            Physics2D.queriesHitTriggers = true;

            if (_othersInSight.Count > 1)
                _othersInSight.Sort((o1, o2) => o1.distance > o2.distance ? 1 : 0);

            ProcessOthersInSight(_othersInSight);
        }

        private float IsOtherVisible(int index)
        {
            Vector2 otherPos = _observableCreatures[index].bounds.center;

            var distance = Vector2.Distance(Position, otherPos);

            if (distance > ViewingDistance)
                return -1f;

            if (Vector2.Angle(LookVector, otherPos) > ViewingAngle / 2)
                return -1f;

            var hits = Physics2D.RaycastAll(Position, otherPos - Position, distance);

            if (hits.Length > 1 && hits[1].collider == _observableCreatures[index])
                return distance;

            return -1f;
        }


        // КАК ВАРИАНТ
        private void LookAround()
        {

            _checkOthersTimer += Time.deltaTime;

            if (_checkOthersTimer < _checkOthersTime)
                return;

            _checkOthersTimer = 0f;

            var viewHits = new List<RaycastHit2D>();

            var LookVectorAngel = Vector2.SignedAngle(new Vector2(1f, 0f), LookVector);

            var rayAngel = LookVectorAngel - ViewingAngle / 2;

            for (; rayAngel < LookVectorAngel + ViewingAngle / 2; rayAngel += 4)
            {
                var radians = Mathf.PI * rayAngel / 180f;

                var rayDirection = new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
                Debug.DrawRay(Position, rayDirection * ViewingDistance, Color.green, 1);

                viewHits.Add(Physics2D.Raycast(Position, rayDirection, ViewingDistance));
            }

            ProcessDetectedObjects(viewHits);
        }
        private void ProcessDetectedObjects(List<RaycastHit2D> detected) { }
    }
}
