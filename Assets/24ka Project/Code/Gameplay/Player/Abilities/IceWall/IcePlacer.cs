using Assets._24ka_Project.Code.Interfaces.Gameplay;
using Code.Gameplay.Systems.ObjectPlacement;
using UnityEngine;

namespace Code.Gameplay.Player.Abilities.IceWall
{
    internal class IcePlacer : IBarrierMaker
    {
        private IceWall _horizontalWall;
        private IceWall _verticalWall;

        private ObjectPlacer _placer;
        private float _distanceFrom = 2f;

        public IcePlacer(IceWall horizontal, IceWall verticcal)
        {
            _horizontalWall = horizontal;
            _verticalWall = verticcal;

            _placer = new ObjectPlacer();
        }

        public void PlaceBarrier(Vector2 direction, Vector2 position)
        {
            TryPlaceWall(direction, position);
        }

        public bool TryPlaceWall(Vector2 direction, Vector2 position)
        {
            var point = position + direction * _distanceFrom;

            if (Mathf.Abs(direction.y) > 0.6f)
                return _placer.TryPlace(_horizontalWall.gameObject, point, 2f);

            if(Mathf.Abs(direction.x) > 0.6f)
                return _placer.TryPlace(_verticalWall.gameObject, point, 2f);

            return false;
        }
    }
}
