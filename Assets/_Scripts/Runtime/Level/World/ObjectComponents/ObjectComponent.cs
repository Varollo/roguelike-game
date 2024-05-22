using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class ObjectComponent : IObjectComponent
    {
        public Vector2Int Position { get; private set; }

        public virtual void OnPositionChange(Vector2Int newPos)
        {
            Position = newPos;
        }

        public virtual void Kill() { }
    }
}