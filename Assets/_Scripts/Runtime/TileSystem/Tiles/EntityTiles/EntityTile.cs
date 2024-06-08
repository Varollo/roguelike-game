using DG.Tweening;
using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class EntityTile : Tile, ITurnListener
    {
        public EntityTile(Transform transform, bool enabled = true) : base(transform, enabled)
        {
            InitComponents();
        }

        #region Component Properties
        protected IMoveProvider MoveProvider { get; private set; }
        protected IMoveValidator MoveValidator { get; private set; }
        protected IMoveAnimator MoveAnimator { get; private set; } 
        #endregion

        #region Component Initializers
        private void InitComponents()
        {
            MoveProvider = InitMoveProvider();
            MoveValidator = InitMoveValidator();
            MoveAnimator = InitMoveAnimator();
        }

        protected virtual IMoveProvider InitMoveProvider() => new NoMoveProvider();
        protected virtual IMoveValidator InitMoveValidator() => new FreeMoveValidator();
        protected virtual IMoveAnimator InitMoveAnimator() => new SetMoveAnimator(); 
        #endregion

        /// <summary>
        /// Moves the transform using <see cref="DOTween"/>
        /// </summary>
        protected override void OnMove(Vector2Int from, Vector2Int to)
        {
            MoveAnimator.OnTileMove(Transform, from, to);
        }

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

        public virtual void OnTurnStart(ulong turnCount)
        {
            // Tell Tile Manager this object started the turn at this position!
            TileManager.TryMove(Position);
        }

        public virtual void OnTurnEnd(ulong turnCount) { }
        #endregion
    }
}
