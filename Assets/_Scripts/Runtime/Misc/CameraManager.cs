using Ribbons.RoguelikeGame.ColorPalette;
using UnityEngine;

namespace Ribbons.RoguelikeGame.Misc
{
    public class CameraManager : IManager, IUpdateListener, IEnableListener
    {
        public delegate void CameraPositionDelegate(Camera camera, Vector3 position);
        public event CameraPositionDelegate OnCameraMove;

        private readonly float _cameraMoveThresshold = 1;

        private Camera _activeCamera;
        private Vector3 _lastCameraPos;

        public Camera GetActiveCamera()
        {
            if (!_activeCamera)
            {
                _activeCamera = Camera.main;

                if (_activeCamera)
                    _activeCamera.backgroundColor = ManagerMaster.GetManager<PaletteManager>().GetColor(ColorID.Black);
            }

            return _activeCamera;
        }

        private float GetMoveDistance(Vector3 oldPos, Vector3 newPos)
        {
            return (newPos - oldPos).sqrMagnitude;
        }

        private void HandleCameraMove(Camera camera, Vector3 camPos)
        {
            _lastCameraPos = camPos;
            OnCameraMove?.Invoke(camera, camPos);
        }

        #region Messager Callbacks
        public void OnInit()
        {
            OnEnable();
        }

        public void OnEnable()
        {
            Camera mainCam = GetActiveCamera();

            if (mainCam)
                _lastCameraPos = mainCam.transform.position;
        }

        public void OnUpdate()
        {
            Camera mainCam = GetActiveCamera();

            if (!mainCam)
                return;

            Vector3 newCameraPos = mainCam.transform.position;

            float moveDist = GetMoveDistance(_lastCameraPos, newCameraPos);

            if (moveDist >= _cameraMoveThresshold * _cameraMoveThresshold)
                HandleCameraMove(mainCam, newCameraPos);
        }

        public void OnDestroy() { } 
        #endregion
    }
}
