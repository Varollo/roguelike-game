using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class SpawnLikeOneBillionOfThisShit : MonoBehaviour
    {
        private static bool spawned = false;
        [SerializeField][Min(0)] private int spawnSize = 100;

        private void Start()
        {
            if (spawned)
                return;
            else
                spawned = true;

            int halfSize = (int)(spawnSize * .5f);

            for (int j = -halfSize; j <= halfSize; j++)
            {
                for (int i = -halfSize; i <= halfSize; i++)
                {
                    if (i == 0 && j == 0)
                        continue;

                    var go = Instantiate(gameObject, transform.parent);
                    go.transform.position = transform.position + new Vector3(i * Random.Range(4, 7), j * Random.Range(4, 7));
                    //go.transform.position = transform.position + new Vector3(i * 1, j * 1);
                }
            }
        }
    }
}
