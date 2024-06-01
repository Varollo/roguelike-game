using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class MoveRandDirBehaviour : MovementBehaviour
    {
        protected override Vector2 GetMovePos(Vector2 iniPos)
        {
            return iniPos + RNGManager.Direction();
        }
    }
}
