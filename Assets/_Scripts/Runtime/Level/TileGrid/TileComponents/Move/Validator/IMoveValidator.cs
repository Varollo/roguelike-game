using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public interface IMoveValidator
    {
        bool Validate(BaseTile tile, Vector2Int toPos);
    }
}
