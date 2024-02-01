using Assets._24ka_Project.Code.Interfaces.Gameplay;
using Code.Gameplay.State;
using Code.Gameplay.Systems.Animation;
using Code.Interfaces.Architecture;
using UnityEngine;

namespace Assets._24ka_Project.Code.Gameplay.State
{
    internal class DefenseState : CreatureStateBase
    {
        private readonly IBarrierMaker _barrierMaker;
        private bool _isBarrierPlaced = false;
        private float _barrierInstallationTimer = 0f;

        public float BarrierInstallationTime { get; set; } = 0f;

        public DefenseState
            (IStateSwitcher switcher,
            DirectionAnimation animation,
            IBarrierMaker barrierMaker = null
            ) : base(switcher, animation)
        {
            _barrierMaker = barrierMaker;
        }

        public override void Start()
        {
            base.Start();

            _isBarrierPlaced = _barrierMaker == null ? true : false;
            _barrierInstallationTimer = BarrierInstallationTime;
        }

        public override void Update(Vector2 lookVector, Vector2 moveVector, Vector2 actionPoint)
        {
            base.Update(lookVector, moveVector, actionPoint);

            if(_isBarrierPlaced == false)
            {
                if(_barrierInstallationTimer > 0)
                {
                    _barrierInstallationTimer -= Time.deltaTime;
                }
                else
                {
                    _barrierMaker.PlaceBarrier(ViewVector, actionPoint);
                    _isBarrierPlaced = true;
                }
            }
        }
    }
}
