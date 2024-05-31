using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public interface IMoveValidator
    {
        bool Validate(Vector2Int fromPos, Vector2Int toPos);
    }
}
