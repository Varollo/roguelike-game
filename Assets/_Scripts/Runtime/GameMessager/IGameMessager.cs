namespace Ribbons.RoguelikeGame
{
    public interface IGameMessager
    {
        void AddListener(IGameMessageListener listener);
        void RemoveListener(IGameMessageListener listener);
    }
}