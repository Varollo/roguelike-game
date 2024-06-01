namespace Ribbons.RoguelikeGame
{
    public static class TurnManager
    {
        public delegate void TurnDelegate(ulong turnCount);

        private static event TurnDelegate TurnStartEvent;
        private static event TurnDelegate TurnActionEvent;
        private static event TurnDelegate TurnEndEvent;

        public static ulong TurnCount { get; private set; }

        public static void NextTurn()
        {
            TurnCount++;
            TurnStartEvent?.Invoke(TurnCount);
            TurnActionEvent?.Invoke(TurnCount);
            TurnEndEvent?.Invoke(TurnCount);
        }

        public static void AddTurnListener(ITurnListener listener)
        {
            TurnStartEvent += listener.OnTurnStart;
            TurnActionEvent += listener.OnTurnAction;
            TurnEndEvent += listener.OnTurnEnd;
        }

        public static void RemoveTurnListener(ITurnListener listener)
        {
            TurnStartEvent -= listener.OnTurnStart;
            TurnActionEvent -= listener.OnTurnAction;
            TurnEndEvent -= listener.OnTurnEnd;
        }
    }
}
