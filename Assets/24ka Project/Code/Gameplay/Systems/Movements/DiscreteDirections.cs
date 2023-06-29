using UnityEngine;

namespace Code.Gameplay.Systems.Movements
{
    internal class DiscreteDirections
    {
        private float _angelStep;

        private int _directionsNumber;
        public int DirectionsNumber
        {
            get => _directionsNumber;
            set
            {
                if (value < 3)
                    _directionsNumber = 3;
                else if(value > 16)
                    _directionsNumber = 16;
                else
                    _directionsNumber = value;

                _angelStep = 360f / _directionsNumber;
            }
        }

        public float Rotation { get; set; }

        public DiscreteDirections(int directionsNumber = 8, float rotation = 0f)
        {
            DirectionsNumber = directionsNumber;
            Rotation = rotation;
        }

        public Vector2 GetVector(Vector2 from, Vector2 to)
        {
            var vectorToTarget = to - from;

            var angelToTargert = Vector2.SignedAngle(new Vector2(1f, 0f), vectorToTarget);

            angelToTargert += _angelStep / 2f - Rotation;

            if (angelToTargert < 0)
                angelToTargert = 360 + angelToTargert;

            angelToTargert = Mathf.Floor(angelToTargert / _angelStep) * _angelStep + Rotation;
            
            var radians = angelToTargert * Mathf.Deg2Rad;

            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        }
    }
}
