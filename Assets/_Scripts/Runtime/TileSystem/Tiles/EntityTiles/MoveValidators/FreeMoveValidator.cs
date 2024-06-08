using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class FreeMoveValidator : IMoveValidator
    {
        public bool CanMove(Vector2Int from, Vector2Int to)
        {
            return from != to;
        }
    }
}
