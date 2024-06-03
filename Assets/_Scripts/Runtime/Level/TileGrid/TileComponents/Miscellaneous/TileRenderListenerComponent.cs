namespace Ribbons.RoguelikeGame
{
    using UnityEngine;

    public class TileRenderListenerComponent : BaseTileComponent
    {
        public delegate void TileRenderDelegate();
        
        public TileRenderDelegate OnEnterScreen;
        public TileRenderDelegate OnExitScreen;

        private Camera _camera;
        private bool _onScreen;

        public TileRenderListenerComponent(BaseTile parentTile, TileRenderDelegate onEnterScreen = null,  TileRenderDelegate onExitScreen = null) : this(parentTile, null, onEnterScreen, onExitScreen)
        {
        }

        public TileRenderListenerComponent(BaseTile parentTile, Camera mainCamera, TileRenderDelegate onEnterScreen = null, TileRenderDelegate onExitScreen = null) : base(parentTile)
        {
            OnEnterScreen = onEnterScreen;
            OnExitScreen = onExitScreen;
            _camera = mainCamera;
        }

        protected Camera Camera => _camera != null ? _camera : (_camera = Camera.main);
        
        public bool OnScreen
        {
            get => _onScreen; private set
            {
                if (_onScreen != value)
                {
                    _onScreen = value;
                    (_onScreen ? OnEnterScreen : OnExitScreen)?.Invoke();
                }
            }
        }

        public override void OnTilePositionMove(Vector2Int newPos)
        {
            base.OnTilePositionMove(newPos);
            OnScreen = Camera.IsPointOnScreen2D(newPos);
        }
    }
}
