namespace Ribbons.RoguelikeGame
{
    public class NPCTileView : TileView
    {
        protected override ITile CreateTile()
        {
            return new NPCTile(transform);
        }
    }
}
