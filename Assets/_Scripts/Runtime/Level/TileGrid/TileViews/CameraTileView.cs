using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class CameraTileView : TileView
    {
        [SerializeField] private TileView _cameraTargetTile;

        protected override ITile CreateTile()
        {
            return new CameraTile(transform, _cameraTargetTile);
        }
    }
}
