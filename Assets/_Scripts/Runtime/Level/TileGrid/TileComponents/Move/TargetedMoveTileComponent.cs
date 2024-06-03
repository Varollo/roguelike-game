using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TargetedMoveTileComponent : MoveTileComponent
    {
        private BaseTile _target;

        public TargetedMoveTileComponent(BaseTile parentTile, BaseTile target = null) : this(parentTile, new FreeMoveValidator(), target)
        {
        }

        public TargetedMoveTileComponent(BaseTile parentTile, IMoveValidator validator, BaseTile target = null) : base(parentTile, validator)
        {
            SetTarget(target);
        }

        protected BaseTile Target { get => _target; private set => _target = value; }
        public bool HasTarget => Target != null;

        public void SetTarget(BaseTile target)
        {
            Target = target;
        }

        protected override Vector2Int ComputeMove()
        {
            return HasTarget
                ? Target.Position - TilePosition
                : Vector2Int.zero;
        }
    }
}
