using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class TileMoveRegistry : IMoveRegistry
    {
        private readonly HashSet<Vector2Int> _pendingMoves = new();

        public void Refresh() => _pendingMoves.Clear();
        public bool TryMove(int x, int y) => _pendingMoves.Add(new(x, y));
        public bool IsFree(int x, int y) => _pendingMoves.Contains(new(x, y));
    }
}
