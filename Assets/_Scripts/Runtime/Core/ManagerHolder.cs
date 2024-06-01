using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class ManagerHolder : UnitySingleton<ManagerHolder>
    {
        private const string RESOURCE_PATH = "Prefabs/Managers/Managers";

        protected override bool KeepOnLoadScene => true;

        [RuntimeInitializeOnLoadMethod]
        private static void OnInitialize()
        {
            if (!HasInstance)
            {
                GameObject prefab = Resources.Load<GameObject>(RESOURCE_PATH);

                if (prefab == null )
                    Debug.LogWarning($"Could not instantiate ManagerHolder prefab at path \"Assets/Resources/{RESOURCE_PATH}\"");
                else
                    Instantiate(prefab);
            }
        }
    }
}
