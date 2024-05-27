namespace Ribbons.RoguelikeGame
{
    public class PlayerTileView : TileView
    {
        protected override ITile CreateTile()
        {
            return new PlayerTile(transform);
        }
    }
}
