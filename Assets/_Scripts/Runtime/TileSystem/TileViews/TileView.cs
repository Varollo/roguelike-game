using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public abstract class TileView : MonoBehaviour 
    {
        public abstract ITile Tile { get; }
    }
    public abstract class TileView<TTile> : TileView where TTile : ITile
    {
        public override ITile Tile => GetTile();

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
