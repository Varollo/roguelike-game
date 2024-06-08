using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public interface IMoveAnimator
    {
        void OnTileMove(Transform tileTransform, Vector2Int from, Vector2Int to);
    }
}
