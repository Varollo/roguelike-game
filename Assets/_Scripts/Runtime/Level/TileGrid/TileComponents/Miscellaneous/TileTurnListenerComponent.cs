namespace Ribbons.RoguelikeGame
{
    using TurnDelegate = TurnManager.TurnDelegate;

    public class TileTurnListenerComponent : BaseTileComponent, ITurnListener
    {
        private TurnDelegate _actionCallback;
        private TurnDelegate _startCallback;
        private TurnDelegate _endCallback;

        public TileTurnListenerComponent(BaseTile parentTile, TurnDelegate actionCallback = null, TurnDelegate startCallback = null, TurnDelegate endCallback = null) : base(parentTile)
        {
            _actionCallback = actionCallback;
            _startCallback = startCallback;
            _endCallback = endCallback;

            TurnManager.AddTurnListener(this);
        }

        public override void OnTileDestroy()
        {
            TurnManager.RemoveTurnListener(this);
        }

        #region External Callbacks
        /* Probably shouldn't use these, just leaving it in to be sure... */
        public void SetAction(TurnManager.TurnDelegate callback) => _actionCallback = callback;
        public void SetStart(TurnManager.TurnDelegate callback) => _startCallback = callback;
        public void SetEnd(TurnManager.TurnDelegate callback) => _endCallback = callback; 
        #endregion

        #region ITurnListener Callbacks
        public void OnTurnAction(ulong turnCount)
        {
            OnAction(turnCount);
            _actionCallback?.Invoke(turnCount);
        }

        public void OnTurnStart(ulong turnCount)
        {
            OnStart(turnCount);
            _startCallback?.Invoke(turnCount);
        }

        public void OnTurnEnd(ulong turnCount)
        {
            OnEnd(turnCount);
            _endCallback?.Invoke(turnCount);
        }
        #endregion

        #region Protected Virtual Callbacks
        protected virtual void OnAction(ulong turnCount) { }
        protected virtual void OnStart(ulong turnCount) { }
        protected virtual void OnEnd(ulong turnCount) { } 
        #endregion
    }
}
