using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TileCollisionManager
    {
        private readonly ITileCollisionChecker DefaultCollisionChecker = new DefaultTileCollisionChecker();

        private readonly Dictionary<Vector2Int, TileCollision> _collisionMap = new();

        public TileCollision GetCollisionAt(Vector2 position)
        {
            Vector2Int gridPos = WorldToGridPos(position);

            if (_collisionMap.TryGetValue(gridPos, out TileCollision col))
                return col;

            return TileCollision.Free;
        }

        public void SetTile(Vector2 position, TileCollision col = TileCollision.Free)
        {
            Vector2Int gridPos = WorldToGridPos(position);
            UpdateTileCollision(gridPos, col);
        }

        public Vector2 SlideTile(Vector2 tilePos, Vector2Int moveDir, int targetLength, ITileCollisionChecker collisionChecker, bool throwOnMissing = true)
        {
            Vector2 fPos = WorldToGridPos(tilePos);

            for (int i = 1; i <= targetLength; i++)
            {
                Vector2Int nPos = WorldToGridPos(tilePos + moveDir * i);

                if (!_collisionMap.ContainsKey(nPos))
                    continue;

                if (MoveTile(tilePos, nPos, collisionChecker, throwOnMissing))
                    break;

                fPos = nPos;
            }

            return fPos;
        }

        public Vector2 SlideTile(Vector2 tilePos, Vector2Int moveDir, int targetLength, bool throwOnMissing = true)
        {
            return SlideTile(tilePos, moveDir, targetLength, DefaultCollisionChecker, throwOnMissing);
        }

        public bool MoveTile(Vector2 oldPos, Vector2 newPos, ITileCollisionChecker collisionChecker, bool throwOnMissing = true)
        {
            Vector2Int oldGridPos = WorldToGridPos(oldPos);

            if (!_collisionMap.ContainsKey(oldGridPos) && throwOnMissing)
                throw new ArgumentException($"Trying to move tile on unregistered position \"{oldPos}\".", nameof(oldPos));

            if (!ValidateMove(oldPos, newPos, collisionChecker, throwOnMissing))
                return false;

            SetTile(WorldToGridPos(newPos), GetCollisionAt(oldPos));
            FreeTile(oldGridPos);

            return true;
        }

        public bool MoveTile(Vector2 oldPos, Vector2 newPos, bool throwOnMissing = true)
        {
            return MoveTile(oldPos, newPos, DefaultCollisionChecker, throwOnMissing);
        }

        private bool ValidateMove(Vector2 oldPos, Vector2 newPos, ITileCollisionChecker collisionChecker, bool throwOnMissing = true)
        {
            TileCollision oldCo = GetCollisionAt(oldPos);
            TileCollision newCo = GetCollisionAt(newPos);

            if (collisionChecker == null && throwOnMissing)
                throw new ArgumentNullException(nameof(collisionChecker), "Collision checker not provided. Leave empty for default instead.");

            return collisionChecker.CheckCollision(oldCo, newCo);
        }

        public bool FreeTile(Vector2 position)
        {
            Vector2Int gridPos = WorldToGridPos(position);
            return _collisionMap.Remove(gridPos);
        }

        private bool UpdateTileCollision(Vector2Int gridPos, TileCollision col)
        {
            if (_collisionMap.TryAdd(gridPos, col))
                return true;

            else if (_collisionMap[gridPos] == col)
                return false;

            _collisionMap[gridPos] = col;
            return true;

        }

        public int GetEntityID(Transform entityTransform)
        {
            return entityTransform.GetInstanceID();
        }

        public Vector2Int WorldToGridPos(Vector2 globalPos) => new()
        {
            x = Mathf.FloorToInt(globalPos.x),
            y = Mathf.FloorToInt(globalPos.y),
        };
    }
}
