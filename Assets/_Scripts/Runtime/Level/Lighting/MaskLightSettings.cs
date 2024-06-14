using System.Linq;
using UnityEngine;

namespace Ribbons.RoguelikeGame.Level.Lighting
{
    [CreateAssetMenu(fileName = "MaskLightSettings", menuName = "Ribbons/Settings/MaskLight Settings")]
    public class MaskLightSettings : ScriptableObject
    {

        [SerializeField] private Sprite[] mipMap;

        private Sprite[] sortedMipMap = null;
        private Sprite _fallbackSprite;

        /// <summary>
        /// Gets a sprite from <see cref="mipMap"/> based on light intensity from 0 to 1.
        /// </summary>
        /// <param name="intensity">Intensity of light from 0 to 1</param>
        public Sprite GetLightSprite(float intensity)
        {
            if (mipMap == null || mipMap.Length == 0)
                return _fallbackSprite ??= Sprite.Create(Texture2D.whiteTexture, new(0, 0, 1, 1), new(0.5f, 0.5f));

            if (sortedMipMap == null || sortedMipMap.Length == 0)
                sortedMipMap = mipMap.OrderBy(spr => spr.rect.width * spr.rect.height).ToArray();

            return sortedMipMap[Mathf.FloorToInt(Mathf.Clamp01(intensity) * (sortedMipMap.Length - 1))];
        }
    }
}
