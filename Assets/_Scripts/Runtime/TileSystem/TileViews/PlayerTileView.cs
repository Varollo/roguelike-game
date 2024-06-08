namespace Ribbons.RoguelikeGame.TileSystem
{
    public class PlayerTileView : TileView<PlayerTile>
    {
        private PlayerTile _tile;

        protected override PlayerTile GetTile() => _tile ??= new(transform);
    }
}
