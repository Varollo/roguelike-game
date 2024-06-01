using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class MoveTileComponent : BaseTileComponent
    {
        private readonly IMoveValidator _validator;

        protected MoveTileComponent(BaseTile parentTile) : this(parentTile, new FreeMoveValidator())
        {
        }

        protected MoveTileComponent(BaseTile parentTile, IMoveValidator validator) : base(parentTile)
        {
            _validator = validator;
        }

        protected IMoveValidator Validator => _validator;

        public Vector2Int GetMove()
        {
            Vector2Int move = ComputeMove();

            if (ValidateMove(move))
                return move;

            return ComputeFallback(move);
        }

        protected virtual Vector2Int ComputeFallback(Vector2Int badMove) => Vector2Int.zero;
        protected virtual bool ValidateMove(Vector2Int move) => Validator.Validate(ParentTile, TilePosition + move);

        protected abstract Vector2Int ComputeMove();
    }
}
