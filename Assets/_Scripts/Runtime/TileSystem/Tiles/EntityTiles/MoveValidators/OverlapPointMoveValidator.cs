using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class OverlapPointMoveValidator : ManagedMoveValidator
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

        public override bool CanMove(Vector2Int from, Vector2Int to)
        {
            return !OverlapPoint(to) && base.CanMove(from, to);
        }

        private bool OverlapPoint(Vector2Int to)
        {
            return _useLayerMask 
                ? Physics2D.OverlapPoint(to, _layerMask) 
                : Physics2D.OverlapPoint(to);
        }
    }
}
