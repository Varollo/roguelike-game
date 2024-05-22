namespace Ribbons.RoguelikeGame
{
    public class DefaultTileCollisionChecker : ITileCollisionChecker
    {
        public bool CheckCollision(TileCollision tile, TileCollision other)
        {
            return (tile & other) != 0;
        }
    }
}
