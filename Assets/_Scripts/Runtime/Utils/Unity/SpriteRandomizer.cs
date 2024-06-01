using System;
using System.Linq;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteRandomizer : MonoBehaviour
    {
        [SerializeField] private bool changeOnStart = true;
        [SerializeField] private SpriteWeightData[] sprites;
        [SerializeField] private bool usePositionAsSeed = false;

        private SpriteRenderer _spriteRenderer;
        private WeightedList<Sprite> _spriteList;

        private SpriteRenderer SpriteRenderer => _spriteRenderer == null 
                ? _spriteRenderer = GetComponent<SpriteRenderer>() 
                : _spriteRenderer;

        private WeightedList<Sprite> SpriteList => _spriteList ??= new(
            sprites.ToDictionary(s => s.Sprite, s => s.Weight));

        private void Start()
        {
            if (changeOnStart)
                RandomizeSprite();
        }

        public void RandomizeSprite() => SpriteRenderer.sprite = usePositionAsSeed
                ? SpriteList.Get(RNGManager.FromSeed(GetSeedFromPosition(transform.position)))
                : SpriteList.Get(RNGManager.Value);

        private static string GetSeedFromPosition(Vector3 position)
        {
            const float FUNNY_NUMBER = 69.0f;
            const float NOT_SO_FUNNY = 0.69f;

            float a = FUNNY_NUMBER * position.x + position.y;
            float b = NOT_SO_FUNNY * position.y - position.x;

            return $"{b + a}";
        }

        [Serializable]
        private struct SpriteWeightData
        {
            public Sprite Sprite;
            [Range(0.0f, 1.0f)] public float Weight;
        }
    }
}
