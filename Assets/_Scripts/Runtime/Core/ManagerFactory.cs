using System;
using System.Collections.Generic;

namespace Ribbons.RoguelikeGame
{
    public class ManagerFactory : IManagerFactory
    {
        private readonly Dictionary<Type, Func<IManager>> _managerMakerTypeMap = new()
        {
        };

        public IManager CreateManager(Type managerType, IGameMessager gameMessager)
        {
            if (!_managerMakerTypeMap.TryGetValue(managerType, out var managerMaker))
                managerMaker = () => CreateManagerAny(managerType);

            IManager manager = managerMaker.Invoke();

            gameMessager.AddListener(manager);
            manager.OnInit();

            return manager;
        }

        private IManager CreateManagerAny(Type managerType) => Activator.CreateInstance(managerType) as IManager;
    }
}
