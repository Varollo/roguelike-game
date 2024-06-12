using System;
using System.Collections.Generic;

namespace Ribbons.RoguelikeGame
{
    public static class ManagerMaster
    {
        private static readonly Dictionary<Type, IManager> _managerTypeMap = new();
        private static readonly IManagerFactory _managerFactory = new ManagerFactory();

        private static IGameMessager GameMessager => UnityMessager.Instance;

        public static TManager GetManager<TManager>() where TManager : class, IManager => GetManager(typeof(TManager)) as TManager;
        public static IManager GetManager(Type type)
        {
            if (!ValidateType(type, out var exception))
                throw exception;

            if (!_managerTypeMap.TryGetValue(type, out var manager))
                _managerTypeMap.Add(type, manager = _managerFactory.CreateManager(type, GameMessager));

            return manager;
        }

        private static bool ValidateType(Type type, out Exception exception)
        {
            exception = null;

            if (!type.IsClass)
                exception = new ArgumentException($"Manager Type \"{type.FullName}\" must be a class.", nameof(type));

            else if (type.GetInterface(nameof(IManager)) == null)
                exception = new ArgumentException($"Manager Type \"{type.FullName}\" must implement \"{nameof(IManager)}\" interface.", nameof(type));

            return exception == null;
        }
    }
}
