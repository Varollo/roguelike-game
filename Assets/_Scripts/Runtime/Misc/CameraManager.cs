using UnityEngine;

namespace Ribbons.RoguelikeGame.Misc
{
    public class CameraManager : UnitySingleton<CameraManager>
    {
        public delegate void CameraPositionDelegate(Camera camera, Vector3 position);
        public event CameraPositionDelegate OnCameraMove;

        [SerializeField] private float cameraMoveThresshold = 1;

        private Camera _activeCamera;
        private Vector3 _lastCameraPos;

        protected override bool KeepOnLoadScene => false;

        public Camera GetActiveCamera() => _activeCamera ? _activeCamera : (_activeCamera = Camera.main);

        private void OnEnable()
        {
            Camera mainCam = GetActiveCamera();

            if (mainCam)
                _lastCameraPos = mainCam.transform.position;
        }

        private void Update()
        {
            Camera mainCam = GetActiveCamera();

            if (!mainCam)
                return;

            Vector3 newCameraPos = mainCam.transform.position;

            float moveDist = GetMoveDistance(_lastCameraPos, newCameraPos);

            if (moveDist >= cameraMoveThresshold * cameraMoveThresshold)
                HandleCameraMove(mainCam, newCameraPos);
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
    }
}
