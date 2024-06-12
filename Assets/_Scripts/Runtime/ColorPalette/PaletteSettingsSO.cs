using UnityEngine;

namespace Ribbons.RoguelikeGame.ColorPalette
{
    [CreateAssetMenu(fileName = "PaletteSettings", menuName = "Ribbons/Settings/Palette Settings")]
    public class PaletteSettingsSO : ScriptableObject
    {
        [SerializeField] private Texture2D defaultPaletteTex;
        [SerializeField] private Color fallbackColor = Color.white;

        public Color FallbackColor => fallbackColor;
        public Texture2D DefaultTexture => defaultPaletteTex;
    }
}
