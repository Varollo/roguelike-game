using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class SOInstanceSingleton : UnitySingleton<SOInstanceSingleton>
    {
        private readonly Dictionary<Type, ScriptableObject> _SOInstances = new();

#if UNITY_EDITOR
        [SerializeField] private List<ScriptableObject> loadedSOInstances = new();
#endif
        protected override bool KeepOnLoadScene => true;

        public static TSelf GetSOInstance<TSelf>() where TSelf : UnitySingletonSO<TSelf>
        {
            return GetOrAddInstance<TSelf>();
        }

        private static TSelf GetOrAddInstance<TSelf>() where TSelf : UnitySingletonSO<TSelf>
        {
            if (!Instance._SOInstances.TryGetValue(typeof(TSelf), out var so))
                Instance._SOInstances.Add(typeof(TSelf), so = LoadOrInitSO<TSelf>());

#if UNITY_EDITOR
            if (!Instance.loadedSOInstances.Contains(so))
                Instance.loadedSOInstances.Add(so);
#endif

            return so as TSelf;
        }

        private static TSelf LoadOrInitSO<TSelf>() where TSelf : UnitySingletonSO<TSelf>
        {
            TSelf temp = Activator.CreateInstance<TSelf>(); // gambi?
            TSelf resource = Resources.Load<TSelf>(temp.ResourcePath);

            if (!resource) // failed to load resource, keep temp.
                return temp;

            Destroy(temp);
            return resource;
        }
    }
}
