using DG.Tweening;
using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class EntityTile : Tile
    {
        private const float MOVE_DURATION = .25f;

        public EntityTile(Transform transform, bool enabled = true) : this(transform, new FreeMoveValidator(), enabled) { }

        public EntityTile(Transform transform, IMoveValidator validator, bool enabled = true) : base(transform, enabled)
        {
            MoveValidator = validator;
        }

        protected IMoveValidator MoveValidator { get; private set; }

        /// <summary>
        /// Moves the transform using <see cref="DOTween"/>
        /// </summary>
        protected override void OnMove(Vector2Int pos)
        {
            Transform.DOMove(new()
            {
                x = pos.x,
                y = pos.y,
                z = Transform.position.z
            }, MOVE_DURATION);
        }
    }
}
