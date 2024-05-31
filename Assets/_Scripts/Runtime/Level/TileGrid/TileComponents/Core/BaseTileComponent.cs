using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class BaseTileComponent : ITileComponent
    {
        public Vector2Int TilePosition { get; private set; }

        public virtual void OnTilePositionMove(Vector2Int newPos)
        {
            TilePosition = newPos;
        }

        public virtual void OnTileDestroy() { }
    }
}