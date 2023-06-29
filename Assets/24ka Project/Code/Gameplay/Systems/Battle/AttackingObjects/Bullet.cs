using Code.Gameplay.Systems.Movements;

namespace Code.Gameplay.Systems.Battle.AttackingObjects
{
    public class Bullet : AttackingObject
    {
        private Movement _movement;

        protected override void Start()
        {
            base.Start();
            _movement = new Movement(transform, Speed);
        }

        protected override void Update()
        {
            base.Update();
            _movement.Move(Direction);
        }
    }
}
