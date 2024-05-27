using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TileView : MonoBehaviour
    {
        private ITile _tile;

        public ITile Tile => _tile;

        private void Awake()
        {
            _tile = CreateTile();
            OnAwake();
        }

        /// <summary>  
        /// Called after awake (after tile creation) 
        /// </summary>
        protected virtual void OnAwake() { }

        /// <summary>
        /// Get a new Tile Instance.
        /// </summary>
        protected virtual ITile CreateTile() => new Tile(transform.position.ToVec2Int());
    }
}
