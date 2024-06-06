using Ribbons.RoguelikeGame.Misc;
using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class Tile : ITile
    {
        private readonly Transform _transform;

        public Tile(Transform transform, bool enabled = true)
        {
            _transform = transform;
            Enabled = enabled;
        }

        protected Transform Transform => _transform;
        protected bool Enabled { get; private set; }

        protected Vector2Int Position => Transform.position.ToVec2Int();

        private void OnCameraMove(Camera camera, Vector3 position)
        {
            OnCameraDeltaChanged(camera);
        }

        private void OnCameraDeltaChanged(Camera camera)
        {
            bool onScreen = Transform.OnScreen2D(camera);
            bool stateChanged = onScreen != Enabled;

            Enabled = onScreen;

            if (!stateChanged)
                return;

            else if (onScreen)
                OnEnterScreen(camera);

            else
                OnExitScreen(camera);
        }

        public void Move(Vector2Int pos)
        {
            if (TileManager.TryMove(pos))
            {
                OnMove(pos);
                OnCameraDeltaChanged(CameraManager.Instance.GetActiveCamera());
            }
        }

        public void OnInit()
        {
            OnEnterScreen(CameraManager.Instance.GetActiveCamera()); // when initialized consider on screen
            Move(Transform.position.ToVec2Int()); // move into place
            OnTileInit();
        }

        public void OnKill()
        {
            OnExitScreen(CameraManager.Instance.GetActiveCamera()); // when killed consider off screen
            OnTileKill();
        }

        /// <summary>
        /// Moves the transform using <see cref="Transform.position"/>
        /// </summary>
        protected virtual void OnMove(Vector2Int pos)
        {
            Transform.position = new()
            {
                x = pos.x,
                y = pos.y,
                z = Transform.position.z
            };
        }

        protected virtual void OnTileInit() 
        {
            CameraManager.Instance.OnCameraMove += OnCameraMove;
        }

        protected virtual void OnTileKill() 
        {
            CameraManager.Instance.OnCameraMove -= OnCameraMove;
        }

        protected virtual void OnEnterScreen(Camera camera) 
        {
            Transform.gameObject.SetActive(true);
        }

        protected virtual void OnExitScreen(Camera camera) 
        {
            Transform.gameObject.SetActive(false);
        }
    }
}
