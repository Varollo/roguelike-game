using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public abstract class TileView<TTile> : MonoBehaviour where TTile : ITile
    {
        private void Start()
        {
            OnTileInit();
            GetTile().OnInit();
        }

        private void OnDestroy()
        {
            OnTileKill();
            GetTile().OnKill();
        }

        /// <summary>
        /// Called before Tile finalization, on <see cref="OnDestroy"/> method.
        /// </summary>
        protected virtual void OnTileKill() { }

        /// <summary>
        /// Called before Tile initialization, on <see cref="Start"/> method.
        /// </summary>
        protected virtual void OnTileInit() { }

        /// <returns>
        /// Referenced <see cref="TTile"/> instance.
        /// </returns>
        protected abstract TTile GetTile();
    }
}
