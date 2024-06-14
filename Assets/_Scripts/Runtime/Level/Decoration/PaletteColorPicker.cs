using Ribbons.RoguelikeGame.ColorPalette;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PaletteColorPicker : MonoBehaviour
    {
        [SerializeField] private ColorID colorID;
        [SerializeField] private bool keepAlpha = true;

        private SpriteRenderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            
            var color = ManagerMaster.GetManager<PaletteManager>().GetColor(colorID);
            
            if (keepAlpha)
                color.a = _renderer.color.a;

            _renderer.color = color;
        }
    }
}
