using UnityEngine;

namespace Ribbons.RoguelikeGame.Level.Lighting
{
#if UNITY_EDITOR
    [ExecuteInEditMode]
#endif
    public class MaskLight : MonoBehaviour
    {
        [SerializeField][Range(0f, 1f)] private float intensity;
        [SerializeField] private bool controlledByAnimator = false;
        [Space]
        [SerializeField] private MaskLightSettings lightSettings;

        private float _lateIntensity;
        private SpriteRenderer _spriteRenderer;
        private SpriteMask _spriteMask;

        /// <summary>
        /// Intensity of light range from 0 to 1.
        /// </summary>
        public float Intensity
        {
            get => intensity;

            set
            {
                _lateIntensity = intensity = Mathf.Clamp01(value);
                UpdateLight();
            }
        }

        public SpriteRenderer SpriteRenderer => _spriteRenderer == null 
            ? (_spriteRenderer = this.GetOrAddComponent<SpriteRenderer>(includeChildren: true)) 
            : _spriteRenderer;

        public SpriteMask SpriteMask => _spriteMask == null
            ? (_spriteMask = this.GetOrAddComponent<SpriteMask>(includeChildren: true))
            : _spriteMask;

        private void Update()
        {
            if (controlledByAnimator && Intensity != _lateIntensity)
            {
                _lateIntensity = Intensity;
                UpdateLight();
            }
        }

        private void UpdateLight()
        {
            if (!lightSettings)
            {
                Debug.LogWarning($"'{nameof(MaskLight)}' component in object '{name}' does not contain a reference to a '{nameof(MaskLightSettings)}' instance and it will be ignored.");
                return;
            }

            Sprite lightSprite = lightSettings.GetLightSprite(Intensity);
            
            SpriteMask.sprite = lightSprite;
            SpriteRenderer.sprite = lightSprite;
        }

        #region Editor Stuff
#if UNITY_EDITOR
        private void OnValidate() => UpdateLight();
#endif 
        #endregion
    }
}
