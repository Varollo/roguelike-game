using Ribbons.RoguelikeGame.ColorPalette;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class PaletteColorPicker : MonoBehaviour
    {
        [SerializeField] private ColorID colorID;

        private SpriteRenderer _renderer;

        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _renderer.color = ManagerMaster.GetManager<PaletteManager>().GetColor(colorID);
        }
    }
}
