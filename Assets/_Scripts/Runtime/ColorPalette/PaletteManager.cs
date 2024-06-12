using System;
using UnityEngine;

namespace Ribbons.RoguelikeGame.ColorPalette
{
    public class PaletteManager : ConfigurableManager<PaletteSettingsSO>
    {
        private Texture2D _paletteTex;
        private Color _fallbackColor;

        private Color[] _colors = null;

        private Color[] Colors => _colors;

        public override string SettingsResourcePath => "Settings/PaletteSettings";

        public override void OnInit()
        {
            _fallbackColor = GetSettings().FallbackColor;
            SetPalette(GetSettings().DefaultTexture);
        }

        public Color[] SetPalette(Texture2D paletteTex)
        {
            _paletteTex = paletteTex;

            Color[] colors = _paletteTex.GetPixels();

            return _colors = colors;
        }

        public Color GetColor(int colorID)
        {
            return ValidateColorID(colorID) ? Colors[colorID] : _fallbackColor;
        }

        public Color GetColor(ColorID colorID)
        {
            return GetColor((int)colorID);
        }

        private bool ValidateColorID(int colorID)
        {
            return Colors != null && Colors.Length > 0 && Colors.Length > colorID;
        }

        protected override void FreeResources()
        {
            _colors = null;
            _paletteTex = null;

            base.FreeResources();
        }

    }
}
