using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class FreeMoveValidator : IMoveValidator
    {
        public bool Validate(Vector2Int fromPos, Vector2Int toPos) => true;
    }
}
