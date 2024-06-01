using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class BaseTileComponent : ITileComponent
    {
        protected BaseTileComponent(BaseTile parentTile)
        {
            ParentTile = parentTile;
            TilePosition = ParentTile.Position;
        }

        public BaseTile ParentTile { get; private set; }
        public Vector2Int TilePosition { get; private set; }

        public virtual void OnTilePositionMove(Vector2Int newPos)
        {
            TilePosition = newPos;
        }

        public virtual void OnTileDestroy() { }
    }
}