using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public interface IObjectComponent
    {
        void Kill();
        void OnPositionChange(Vector2Int newPos);
    }
}
