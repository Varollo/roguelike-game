using System;

namespace Ribbons.RoguelikeGame
{
    public interface IManagerFactory
    {
        IManager CreateManager(Type managerType, IGameMessager gameMessager);
    }
}
