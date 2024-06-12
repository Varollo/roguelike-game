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
                SetAsInstance(this);

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

        private void SetAsInstance(TSelf instance)
        {
            _instance = instance;
            
            OnCreateInstance();
            DontDestroyInstance(instance);
        }

        private static TSelf CreateInstance()
        {
            TSelf instance = new GameObject(typeof(TSelf).Name).AddComponent<TSelf>();
            
            instance.OnCreateInstance();
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

        protected virtual void OnCreateInstance() { }

        public static implicit operator TSelf(UnitySingleton<TSelf> a) => (TSelf)a;
    }
}
