using System.Linq;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TileFreeMoveRule : ITileMoveRule
    {
        public bool CanMove(Vector2Int from, Vector2Int to)
        {
            return true;
        }
    }
}
