namespace Ribbons.RoguelikeGame
{
    public interface ITileCollisionChecker
    {
        bool CheckCollision(TileCollision tile, TileCollision other);
    }
}
