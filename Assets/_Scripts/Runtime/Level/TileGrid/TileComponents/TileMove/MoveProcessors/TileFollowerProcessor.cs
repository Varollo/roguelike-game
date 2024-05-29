using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TileFollowerProcessor : ITileMoveProcessor
    {
        public Vector2Int ProcessMove(Vector2Int currentPos, Vector2Int targetPos)
        {
            int x = GetMoveDir(targetPos.x - currentPos.x);
            int y = x != 0 ? 0 : GetMoveDir(targetPos.y - currentPos.y);
            
            return new(x, y);
        }

        private int GetMoveDir(int delta) => delta != 0 ? (int)Mathf.Sign(delta) : 0;
    }
}
