using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class PlayerTile : EntityTile
    {
        public PlayerTile(Transform transform, bool enabled = true) : base(transform, enabled) { }

        protected override IMoveProvider InitMoveProvider() => new SwipeInputMoveProvider();
        protected override IMoveValidator InitMoveValidator() => new OverlapPointMoveValidator();
    }
}
