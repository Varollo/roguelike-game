using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class SetMoveAnimator : IMoveAnimator
    {
        public void OnTileMove(Transform tileTransform, Vector2Int from, Vector2Int to)
        {
            tileTransform.position = new(to.x, to.y, tileTransform.position.z);
        }
    }
}
