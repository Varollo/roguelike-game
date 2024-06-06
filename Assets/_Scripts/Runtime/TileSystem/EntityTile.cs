using DG.Tweening;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class EntityTile : Tile, ITurnListener
    {
        private const float MOVE_DURATION = .25f;

        public EntityTile(Transform transform, bool enabled = true) : base(transform, enabled)
        {
            MoveProvider = InitMoveProvider();
            MoveValidator = InitMoveValidator();
        }

        public EntityTile(Transform transform, IMoveProvider moveProvider, IMoveValidator moveValidator, bool enabled = true) : base(transform, enabled)
        {
            MoveProvider = moveProvider;
            MoveValidator = moveValidator;
        }

        protected IMoveProvider MoveProvider { get; }
        protected IMoveValidator MoveValidator { get; }

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

        protected virtual IMoveProvider InitMoveProvider() => new NoMoveProvider();
        protected virtual IMoveValidator InitMoveValidator() => new FreeMoveValidator();

        #region Enter/Exit Screen Callbacks
        protected override void OnEnterScreen(Camera camera)
        {
            base.OnEnterScreen(camera);
            TurnManager.AddTurnListener(this);
        }

        protected override void OnExitScreen(Camera camera)
        {
            base.OnExitScreen(camera);
            TurnManager.RemoveTurnListener(this);
        } 
        #endregion

        #region Turn Listener Callbacks
        public virtual void OnTurnAction(ulong turnCount) 
        {
            Vector2Int movePos = MoveProvider.GetMovePosition(Position, MoveValidator);
            Move(movePos);
        }

        public virtual void OnTurnStart(ulong turnCount) { }
        public virtual void OnTurnEnd(ulong turnCount) { }
        #endregion
    }
}
