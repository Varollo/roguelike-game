using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class TileManager : UnitySingleton<TileManager>, ITurnListener
    {
        private readonly IMoveRegistry _moveRegistry = new TileMoveRegistry();
        
        protected override bool KeepOnLoadScene => false;

        private void OnEnable() => TurnManager.AddTurnListener(this);
        private void OnDisable() => TurnManager.RemoveTurnListener(this);

        public static bool TryMove(Vector2Int move) => Instance._moveRegistry.TryMove(move.x, move.y);
        public static bool CanMove(Vector2Int move) => Instance._moveRegistry.IsFree(move.x, move.y);

        public void OnTurnEnd(ulong turnCount) => _moveRegistry.Refresh();

        #region Empty ITurnListener Callbacks
        public void OnTurnStart(ulong turnCount) { }
        public void OnTurnAction(ulong turnCount) { } 
        #endregion
    }
}
