using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class UnitySingleton<TSelf> : MonoBehaviour where TSelf : UnitySingleton<TSelf>
    {
        private static TSelf _instance;
        public static TSelf Instance => _instance = _instance != null ? _instance : CreateInstance();

        protected virtual void Awake() => DestroyIfDuplicateInstance();

        private static TSelf CreateInstance()
        {
            TSelf instance = new GameObject(nameof(TSelf)).AddComponent<TSelf>();
            DontDestroyOnLoad(instance);
            return instance;
        }

        private void DestroyIfDuplicateInstance()
        {
            if (_instance == null || _instance == this)
            {
                _instance = this;
                return;
            }
            
            if (gameObject.GetComponents<MonoBehaviour>().Length == 1)
                Destroy(gameObject);
            else
                Destroy(this);
        }

        public static implicit operator TSelf(UnitySingleton<TSelf> a) => (TSelf)a;
    }
}
