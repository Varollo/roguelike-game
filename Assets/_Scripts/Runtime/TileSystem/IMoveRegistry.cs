namespace Ribbons.RoguelikeGame.TileSystem
{
    public interface IMoveRegistry
    {
        void Refresh();
        bool IsFree(int x, int y);
        bool SetFree(int x, int y);
        bool TryMove(int x, int y);
    }
}
