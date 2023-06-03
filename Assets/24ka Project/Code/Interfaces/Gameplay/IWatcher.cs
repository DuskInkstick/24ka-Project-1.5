using UnityEngine;

namespace Code.Interfaces.Gameplay
{
    internal interface IWatcher
    {
        Vector2 ViewDirection { get; }
        void Look(Vector2 direction);
    }
}
