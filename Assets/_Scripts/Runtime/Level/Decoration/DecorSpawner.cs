using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class DecorSpawner : MonoBehaviour
    {
        [SerializeField][Min(1)] private Vector2Int spawnAreaSize = new(7, 13);
        [SerializeField][Range(0f, 1f)] private float chanceToSpawn = 0.3f;
        [SerializeField] private List<MapDecor> prefabsToSpawn;

        private WeightedList<MapDecor> _decorWeightedList;

        private WeightedList<MapDecor> DecorWeightedList => _decorWeightedList ??=
            new(prefabsToSpawn.ToDictionary(
                prefab => prefab,
                prefab => prefab.SpawnWeight));

        private void Start() => CreateDecorInstances();

        private void CreateDecorInstances()
        {
            Vector2 offCenter = (spawnAreaSize - Vector2.one) * -0.5f; // => move the grid to the side to center it on local(0,0)
            Vector2 decorWarpPos = (Vector2)spawnAreaSize * 0.5f; // => ideally half camera viewport, used to warp faraway decors
            Camera cam = Camera.main;

            for (int i = 0; i < spawnAreaSize.x; i++)
            {
                for (int j = 0; j < spawnAreaSize.y; j++)
                {
                    float rand = RNGManager.FromSeed(GetSeedFromPosition(offCenter, i, j));

                    if (CanSpawnDecor(rand))
                        SpawnDecor(DecorWeightedList.Get(rand), new(i + offCenter.x, j + offCenter.y)
                            ).Init(cam, decorWarpPos);
                }
            }
        }

        private MapDecor SpawnDecor(MapDecor prefab, Vector2 position)
        {
            if (prefab == null)
                return null;

            MapDecor decor = Instantiate(prefab);
            SetupDecorTransform(decor, position);

            return decor;
        }

        private bool CanSpawnDecor(float r)
        {
            return r <= chanceToSpawn;
        }

        private void SetupDecorTransform(MapDecor instance, Vector2 localPos)
        {
            instance.transform.parent = transform;
            instance.transform.localPosition = localPos;
        }

        private static string GetSeedFromPosition(Vector2 offCenter, int row, int col)
        {
            /* [===EXPLANATION===]
             * Completly arbritrary, mostly kept tweaking it to
             * reduce the "visible patern" until it looked good.
            */

            const float FUNNY_NUMBER = 69.0f;
            const float NOT_SO_FUNNY = 0.69f;

            float a = FUNNY_NUMBER * row * (offCenter.y + col);
            float b = NOT_SO_FUNNY * col * (offCenter.x + row);

            return $"{b + a}";
        }

        #region Debug Gizmos
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, (Vector2)spawnAreaSize);
        }
        #endregion
    }
}
