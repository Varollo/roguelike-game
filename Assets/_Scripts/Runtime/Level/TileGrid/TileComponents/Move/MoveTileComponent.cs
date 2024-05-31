using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class MoveTileComponent : BaseTileComponent
    {
        private readonly IMoveValidator _validator;

        public MoveTileComponent() : this(new FreeMoveValidator())
        { 
        }

        public MoveTileComponent(IMoveValidator validator)
        {
            _validator = validator;
        }

        protected IMoveValidator Validator => _validator;

        public Vector2Int GetMove()
        {
            Vector2Int move = ComputeMove();

            if (Validator.Validate(TilePosition, TilePosition + move))
                return move;

            return ComputeFallback(move);
        }

        protected virtual Vector2Int ComputeFallback(Vector2Int badMove) => Vector2Int.zero;
        protected abstract Vector2Int ComputeMove();
    }
}
