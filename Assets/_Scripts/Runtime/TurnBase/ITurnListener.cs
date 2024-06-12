namespace Ribbons.RoguelikeGame
{
    public interface ITurnListener
    {
        void OnTurnStart(ulong turnCount);
        void OnTurnAction(ulong turnCount);
        void OnTurnEnd(ulong turnCount);
    }
}
