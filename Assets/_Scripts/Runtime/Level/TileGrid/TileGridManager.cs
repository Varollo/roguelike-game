using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    // consider not being static (1 world p/ scene)
    public static class TileGridManager
    {
        // take a look at bidirectional maps / dictionaries.
        private static readonly Dictionary<Vector2Int, List<ITile>> _pos2tileMap = new();
        private static readonly Dictionary<ITile, Vector2Int> _tile2posMap = new();

        #region Get Tile / Position
        /// <summary>
        /// Attempts to find tile at position (<paramref name="x"/>, <paramref name="y"/>)
        /// </summary>
        /// <param name="tile">found tile, null if fails</param>
        /// <returns>true if tile found at position, false if it's free</returns>
        public static bool TryGetTile(int x, int y, out ITile tile)
        {
            tile = GetTile(x, y);
            return tile != null;
        }

        /// <summary>
        /// Attempts to find all tiles at position (<paramref name="x"/>, <paramref name="y"/>)
        /// </summary>
        /// <param name="tiles">found tiles, null if fails</param>
        /// <returns>true if any tiles found at position, false if it's free</returns>
        public static bool TryGetAllTiles(int x, int y, out List<ITile> tiles)
        {
            tiles = GetAllTiles(x, y);
            return tiles != null;
        }

        /// <summary>
        /// Retrieves first managed tile at position (<paramref name="x"/>, <paramref name="y"/>)
        /// </summary>
        /// <returns>if position is free returns null, otherwise returns FIRST tile found.</returns>
        public static ITile GetTile(int x, int y)
        {
            List<ITile> tiles = GetAllTiles(x, y);
            return tiles != null && tiles.Count > 0 ? tiles[0] : null;
        }

        /// <summary>
        /// Retrieves all managed tiles at position (<paramref name="x"/>, <paramref name="y"/>)
        /// </summary>
        /// <returns>if position is free returns null, otherwise returns all tiles found.</returns>
        public static List<ITile> GetAllTiles(int x, int y)
        {
            if (_pos2tileMap.TryGetValue(new(x, y), out var tiles))
                return tiles;
            return null;
        }

        /// <summary>
        /// Retrieves the registered position of given <paramref name="tile"/>. Will throw if tile is not being managed.
        /// </summary>
        /// <returns>registered tile position</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public static Vector2Int GetTilePosition(ITile tile)
        {
            if (_tile2posMap.TryGetValue(tile, out var pos))
                return pos;
            else throw new KeyNotFoundException(
                $"Tile not managed by {nameof(TileGridManager)} instance. No position registered for provided tile type '{tile.GetType().FullName}'."
            );
        }
        #endregion

        #region Add or Remove Tile
        /// <summary>
        /// Sets an tile to a designated position.
        /// </summary>
        public static void SetTile(ITile tile, int x, int y)
        {
            Vector2Int newPos = new(x, y);

            // if position is the same, ignore call.
            if (_tile2posMap.TryGetValue(tile, out var oldPos))
            {
                if (oldPos == newPos)
                    return;

                RemoveTileFromPosition(tile, oldPos);
            }

            AddTileToPosition(tile, newPos);
            RelayPositionChange(tile, newPos);
        }

        /// <summary>
        /// Stops managing given tile '<paramref name="tile"/>'.
        /// </summary>
        /// <returns>true if object was removed, false if object wasn't being managed.</returns>
        public static bool DestroyTile(ITile tile)
        {
            if (RemoveTileFromPosition(tile, _tile2posMap[tile]) && _tile2posMap.Remove(tile))
            {
                RelayTileDestroy(tile);

                return true;
            }
            return false;
        }

        /// <summary>
        /// Calls the <see cref="ITileComponent.OnTileDestroy()"/> for all tile components first,
        /// then <see cref="ITile.OnDestroy()"/> to the <paramref name="tile"/>.
        /// </summary>
        private static void RelayTileDestroy(ITile tile)
        {
            foreach (var comp in tile)
                comp.OnTileDestroy();

            tile.OnDestroy();
        }

        /// <summary>
        /// Calls the <see cref="ITileComponent.OnTilePositionMove(Vector2Int)"/> for all tile components first,
        /// then <see cref="ITile.OnPositionMove(Vector2Int)"/> to the <paramref name="tile"/>.
        /// </summary>
        private static void RelayPositionChange(ITile tile, Vector2Int pos)
        {
            foreach (ITileComponent component in tile)
                component.OnTilePositionMove(pos);

            tile.OnPositionMove(pos);
        }

        /// <summary>
        /// Adds object to position list or creates it. 
        /// </summary>
        private static void AddTileToPosition(ITile tile, Vector2Int pos)
        {
            _tile2posMap[tile] = pos;

            if (_pos2tileMap.TryGetValue(pos, out var newPosTiles))
                newPosTiles.Add(tile);
            else
                _pos2tileMap.Add(pos, new() { tile });
        }

        /// <summary> 
        /// Removes object from position list, then checks if list is empty and removes it.
        /// </summary>
        private static bool RemoveTileFromPosition(ITile tile, Vector2Int pos)
        {
            if (_pos2tileMap.TryGetValue(pos, out var posTiles))
            {
                posTiles.Remove(tile);

                if (posTiles.Count == 0)
                    _pos2tileMap.Remove(pos);

                return true;
            }

            return false;
        }
        #endregion

        #region Check for Tile / Position
        /// <summary>
        /// Checks if any tile is at position (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <returns>true if at least 1 tile at (<paramref name="x"/>, <paramref name="y"/>), false if none.</returns>
        public static bool HasTile(int x, int y)
        {
            List<ITile> tileList = _pos2tileMap[new(x, y)];
            return tileList != null && tileList.Count > 0;
        }

        /// <summary>
        /// Checks if an tile '<paramref name="tile"/>' is currently being managed.
        /// </summary>
        /// <returns>true if tile is managed, false if not.</returns>
        public static bool HasTile(ITile tile)
        {
            return _tile2posMap.ContainsKey(tile);
        }

        /// <summary>
        /// Casts a ray, starting from <paramref name="origin"/>, 
        /// in a given <paramref name="direction"/>, 
        /// up to <paramref name="maxLength"/> 
        /// until it hits any tile.
        /// </summary>
        /// <param name="origin">starting point [exclusive]</param>
        /// <param name="direction">direction to march</param>
        /// <param name="maxLength">max positions to check</param>
        /// <param name="hitPoint">point with tile detected, if it fails defaults to <paramref name="origin"/></param>
        /// <returns>tile detected, if it fails retuns null instead</returns>
        public static ITile Raycast(Vector2Int origin, Vector2Int direction, int maxLength, out Vector2Int hitPoint)
        {
            hitPoint = origin;

            for (int i = 1; i <= maxLength; i++)
            {
                hitPoint = origin + direction * i;

                if (TryGetTile(hitPoint.x, hitPoint.y, out var tile))
                    return tile;
            }

            return null;
        }
        #endregion
    }
}
