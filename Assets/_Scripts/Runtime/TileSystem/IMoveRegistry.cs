namespace Ribbons.RoguelikeGame.TileSystem
{
    public interface IMoveRegistry
    {
        bool IsFree(int x, int y);
        void Refresh();
        bool TryMove(int x, int y);
    }
}
