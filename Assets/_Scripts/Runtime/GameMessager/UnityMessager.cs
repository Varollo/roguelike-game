using System;
using System.Collections.Generic;

namespace Ribbons.RoguelikeGame
{
    /// <TODO>
    /// Separar lógica em 3 classes:
    /// - Implementação do IGameMessager
    /// - Gerencialmento de Callbacks (dict)
    /// - Conexão com as Unity Messages (Update/OnEnable/etc.)
    /// </TODO>

    public class UnityMessager : UnitySingleton<UnityMessager>, IGameMessager
    {
        private readonly Dictionary<Type, Action> _listenerTypeMap = new();

        protected override bool KeepOnLoadScene => true;

        public void AddListener(IGameMessageListener listener)
        {
            if (listener is IUpdateListener updater)
                AddCallback<IUpdateListener>(updater.OnUpdate);

            AddCallback<IGameMessageListener>(listener.OnDestroy);
        }

        public void RemoveListener(IGameMessageListener listener)
        {
            if (listener is IUpdateListener updatable)
                RemoveCallback<IUpdateListener>(updatable.OnUpdate);

            RemoveCallback<IGameMessageListener>(listener.OnDestroy);
        }

        private void AddCallback<TMessageType>(Action callback) where TMessageType : IGameMessageListener
        {
            Type type = typeof(TMessageType);

            // if type already in map, add callback!
            if (!_listenerTypeMap.TryAdd(type, callback))
                _listenerTypeMap[type] += callback;
        }

        private void RemoveCallback<TMessageType>(Action callback) where TMessageType : IGameMessageListener
        {
            Type type = typeof(TMessageType);

            // if type already in map, remove callback!
            if (!_listenerTypeMap.TryAdd(type, callback))
                _listenerTypeMap[type] -= callback;
        }

        private void OnDestroy() => RaiseMessage<IGameMessageListener>();
        private void OnEnable() => RaiseMessage<IEnableListener>();
        private void Update() => RaiseMessage<IUpdateListener>();

        private void RaiseMessage<TMessageType>() where TMessageType : IGameMessageListener => GetMessage<TMessageType>()?.Invoke();
        private Action GetMessage<TMessageType>() where TMessageType : IGameMessageListener => _listenerTypeMap.TryGetValue(typeof(TMessageType), out Action message) ? message : null;
    }
}
