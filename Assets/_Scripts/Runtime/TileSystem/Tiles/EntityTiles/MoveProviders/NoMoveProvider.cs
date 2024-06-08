using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class NoMoveProvider : IMoveProvider
        {
            public Vector2Int GetMovePosition(Vector2Int origin, IMoveValidator moveValidator)
            {
                return origin;
            }
        }
}
