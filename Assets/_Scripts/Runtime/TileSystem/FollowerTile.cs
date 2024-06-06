using Ribbons.RoguelikeGame.Misc;
using System;
using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class FollowerTile : EntityTile
    {
        private FollowTargetMoveProvider _followTargetMoveProvider;

        public FollowerTile(Transform transform, bool enabled = true) : base(transform, enabled) { }
        public FollowerTile(Transform transform, IMoveProvider moveProvider, IMoveValidator moveValidator, bool enabled = true) : base(transform, moveProvider, moveValidator, enabled) { }

        public FollowerTile(Transform transform, Tile target, bool enabled = true) : base(transform, enabled)
        {
            Target = target;
        }

        public FollowerTile(Transform transform, Tile target, IMoveProvider moveProvider, IMoveValidator moveValidator, bool enabled = true) : base(transform, moveProvider, moveValidator, enabled)
        {
            Target = target;
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

        protected override IMoveProvider InitMoveProvider()
        {
            return _followTargetMoveProvider = new FollowTargetMoveProvider(Transform);
        }
    }
}
