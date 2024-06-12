using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class UnitySingletonSO<TSelf> : ScriptableObject where TSelf : UnitySingletonSO<TSelf>
    {
        public static TSelf Instance => SOInstanceSingleton.GetSOInstance<TSelf>();

        /// <summary>
        /// Path to SO instance in resources folder.
        /// </summary>
        public abstract string ResourcePath { get; }

        public static implicit operator TSelf(UnitySingletonSO<TSelf> a) => (TSelf)a;
    }
}
