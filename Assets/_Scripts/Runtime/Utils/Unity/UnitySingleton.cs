using UnityEngine;


namespace Ribbons.RoguelikeGame
{
    public abstract class UnitySingleton<TSelf> : MonoBehaviour where TSelf : UnitySingleton<TSelf>
    {
        private static TSelf _instance;

        public static TSelf Instance => _instance = HasInstance ? _instance : CreateInstance();
        public static bool HasInstance => _instance != null;

        /// <summary>
        /// If true, calls DontDestroyOnLoad on awake. (by default)
        /// </summary>
        protected abstract bool KeepOnLoadScene { get; }

        protected virtual void Awake()
        {
            CheckForDuplicate(destroyIfDupl: true);
        }

        protected void CheckForDuplicate(bool destroyIfDupl = true)
        {
            if (_instance == null)
                SetInstance(this);

            else if (_instance != this && destroyIfDupl)
                DestroyThis();            
        }

        private void DestroyThis()
        {
            if (gameObject.GetComponents<MonoBehaviour>().Length == 1)
                Destroy(gameObject);

            else
                Destroy(this);
        }

        private void SetInstance(TSelf instance)
        {
            _instance = instance;
            DontDestroyInstance(instance);
        }

        private static TSelf CreateInstance()
        {
            TSelf instance = new GameObject(nameof(TSelf)).AddComponent<TSelf>();
            DontDestroyInstance(instance);
            return instance;
        }

        private static void DontDestroyInstance(TSelf instance)
        {
            if (!instance.KeepOnLoadScene)
                return;

            instance.transform.parent = null;
            DontDestroyOnLoad(instance);
        }

        public static implicit operator TSelf(UnitySingleton<TSelf> a) => (TSelf)a;
    }
}
