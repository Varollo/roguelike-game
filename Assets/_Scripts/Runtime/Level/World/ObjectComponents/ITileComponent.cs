using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public interface ITileComponent
    {
        void OnTileDestroy();
        void OnTilePositionMove(Vector2Int newPos);
    }
}
