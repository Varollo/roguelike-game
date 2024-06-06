using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class OverlapPointMoveValidator : IMoveValidator
    {
        private readonly int _layerMask;
        private readonly bool _useLayerMask;

        public OverlapPointMoveValidator()
        {
            _useLayerMask = false;
        }

        public OverlapPointMoveValidator(int layerMask)
        {
            _layerMask = layerMask;
            _useLayerMask = true;
        }

        public bool CanMove(Vector2Int from, Vector2Int to)
        {
            return _useLayerMask 
                ? !Physics2D.OverlapPoint(to, _layerMask) 
                : !Physics2D.OverlapPoint(to);
        }
    }
}
