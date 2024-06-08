using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class FollowerTile : EntityTile
    {
        private FollowTargetMoveProvider _followTargetMoveProvider;

        public FollowerTile(Transform transform, bool allowDiagonals = true, bool enabled = true) : this(transform, null, allowDiagonals, enabled) { }
        public FollowerTile(Transform transform, Tile target, bool allowDiagonals = true, bool enabled = true) : base(transform, enabled)
        {
            Target = target;
            FollowTargetMoveProvider.AllowDiagonals = allowDiagonals;
        }

        private Tile Target { get; set; }
        private FollowTargetMoveProvider FollowTargetMoveProvider => 
            _followTargetMoveProvider ??= MoveProvider as FollowTargetMoveProvider;

        public void SetTarget(Tile tile)
        {
            Target = tile;
        }

        protected override void OnTileInit()
        {
            base.OnTileInit();
            FollowTargetMoveProvider.SetTarget(Target);
        }

        protected override IMoveProvider InitMoveProvider() => new FollowTargetMoveProvider(Transform);
        protected override IMoveValidator InitMoveValidator() => new ManagedMoveValidator();
        protected override IMoveAnimator InitMoveAnimator() => new TweenMoveAnimator();
    }
}
