namespace Ribbons.RoguelikeGame
{
    public class StaticTileView : TileView 
    {
        protected override ITile CreateTile()
        {
            return new TransformTile(transform);
        }
    }
}
