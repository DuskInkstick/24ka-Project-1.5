using Code.Gameplay.Systems.ObjectPlacement;
using UnityEngine;

namespace Code.Gameplay.Player.Abilities.IceWall
{
    internal class IcePlacer
    {
        private Transform _character;
        private IceWall _horizontalWall;
        private IceWall _verticalWall;

        private ObjectPlacer _placer;
        private float _distanceFrom = 2f;

        public IcePlacer(Transform character, IceWall horizontal, IceWall verticcal)
        {
            _character = character;
            _horizontalWall = horizontal;
            _verticalWall = verticcal;

            _placer = new ObjectPlacer();
        }

        public bool TryPlaceWall(Vector2 direction)
        {
            var point = new Vector2(
                    _character.position.x,
                    _character.position.y) + direction * _distanceFrom;

            if (Mathf.Abs(direction.y) > 0.6f)
                return _placer.TryPlace(_horizontalWall.gameObject, point, 2f);

            if(Mathf.Abs(direction.x) > 0.6f)
                return _placer.TryPlace(_verticalWall.gameObject, point, 2f);

            return false;
        }
    }
}
