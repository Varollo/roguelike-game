using System;
using UnityEngine;
using UnityEngine.Events;

namespace Ribbons.RoguelikeGame
{
    public class MapDecor : MonoBehaviour
    {
        [Header("Spawn Info")]
        [SerializeField][Range(0f, 1f)] private float spawnWeight;
        [Header("Warp Info")]
        [SerializeField][Range(0f, 1f)] float warpPositionScaler = 1f;
        [SerializeField] private bool ignoreZOnWarp = true;
        [Header("Warp Events")]
        [SerializeField] private UnityEvent onWarp;

        public float SpawnWeight => spawnWeight;

        private Vector3 _bounds;
        private Camera _mainCam;

        public void Init(Camera cam, Vector2 maxDistToWarp)
        {
            _bounds = maxDistToWarp;
            _mainCam = cam;
        }

        private void Update()
        {
            if (TryWarp())
                onWarp?.Invoke();
        }

        private bool TryWarp()
        {
            Vector3 delta = _mainCam.transform.position - transform.position;
            
            if (ignoreZOnWarp)
                delta.z = 0f;
            
            Vector3 offset = GetWarpOffset(delta);

            if (offset == Vector3.zero)
                return false;

            transform.position += offset;
            return true;
        }

        private Vector3 GetWarpOffset(Vector3 delta)
        {
            float doubleScaler = 2f * warpPositionScaler;
            return new()
            {
                x = CalculateWarpPoint(doubleScaler, _bounds.x, delta.x),
                y = CalculateWarpPoint(doubleScaler, _bounds.y, delta.y)
            };
        }

        private float CalculateWarpPoint(float scaler, float offset, float delta) =>
            Mathf.Abs(delta) >= offset ? scaler * offset * Mathf.Sign(delta) : 0.0f;
    }
}
