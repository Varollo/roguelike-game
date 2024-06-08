using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public class FollowerTileView : TileView<FollowerTile>
    {
        [SerializeField] private TileView targetView;
        [SerializeField] private bool allowDiagonals = true;

        private FollowerTile _tile;

        protected override FollowerTile GetTile()
        {
            return _tile ??= new(transform, allowDiagonals);
        }

        protected override void OnTileInit()
        {
            GetTile().SetTarget(targetView.Tile as Tile);
        }
    }
}
