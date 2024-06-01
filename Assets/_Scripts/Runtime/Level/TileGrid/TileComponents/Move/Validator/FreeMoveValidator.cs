using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class FreeMoveValidator : IMoveValidator
    {
        public bool Validate(BaseTile tile, Vector2Int toPos) => true;
    }
}
