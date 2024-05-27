﻿using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TileComponent : ITileComponent
    {
        public Vector2Int Position { get; private set; }

        public virtual void OnTilePositionMove(Vector2Int newPos)
        {
            Position = newPos;
        }

        public virtual void OnTileDestroy() { }
    }
}