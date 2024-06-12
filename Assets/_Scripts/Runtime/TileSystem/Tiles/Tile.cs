using Ribbons.RoguelikeGame.Misc;
using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class Tile : ITile
    {
        private readonly Transform _transform;

        public Tile(Transform transform, bool enabled = true)
        {
            Enabled = enabled;
            Position = transform.position.ToVec2Int();
            _transform = transform;
        }

        protected Transform Transform => _transform;
        
        protected bool Enabled { get; private set; }
        public Vector2Int Position { get; private set;}

        private void OnCameraMove(Camera camera, Vector3 position)
        {
            TriggerOffScreenCheck(camera);
        }

        protected void TriggerOffScreenCheck(Camera camera)
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
            OnMove(Position, Position = pos);
            TriggerOffScreenCheck(ManagerMaster.GetManager<CameraManager>().GetActiveCamera());
        }

        public void OnInit()
        {
            OnEnterScreen(ManagerMaster.GetManager<CameraManager>().GetActiveCamera()); // when initialized consider on screen
            Move(Transform.position.ToVec2Int()); // move into place
            OnTileInit();
        }

        public void OnKill()
        {
            OnExitScreen(ManagerMaster.GetManager<CameraManager>().GetActiveCamera()); // when killed consider off screen
            OnTileKill();
        }

        /// <summary>
        /// Moves the transform using <see cref="Transform.position"/>
        /// </summary>
        protected virtual void OnMove(Vector2Int from, Vector2Int pos)
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
            ManagerMaster.GetManager<CameraManager>().OnCameraMove += OnCameraMove;
        }

        protected virtual void OnTileKill() 
        {
            ManagerMaster.GetManager<CameraManager>().OnCameraMove -= OnCameraMove;
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
