using Ribbons.RoguelikeGame.ColorPalette;
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
        private WeightedList<SpriteInfoData> _spriteList;

        private SpriteRenderer SpriteRenderer => _spriteRenderer == null 
                ? _spriteRenderer = GetComponent<SpriteRenderer>() 
                : _spriteRenderer;

        private WeightedList<SpriteInfoData> SpriteList => _spriteList ??= new(
            sprites.ToDictionary(s => new SpriteInfoData(s.Sprite, s.Color), s => s.Weight));

        private void Start()
        {
            if (changeOnStart)
                RandomizeSprite();
        }

        public void RandomizeSprite()
        {
            SpriteInfoData spriteInfo = usePositionAsSeed
                            ? SpriteList.Get(RNGManager.FromSeed(GetSeedFromPosition(transform.position)))
                            : SpriteList.Get(RNGManager.Value);
            
            SpriteRenderer.sprite = spriteInfo.Sprite;
            SpriteRenderer.color = ManagerMaster.GetManager<PaletteManager>().GetColor(spriteInfo.Color);
        }

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
            public ColorID Color;
            [Range(0.0f, 1.0f)] public float Weight;
        }

        private struct SpriteInfoData
        {
            public Sprite Sprite;
            public ColorID Color;

            public SpriteInfoData(Sprite sprite, ColorID color)
            {
                Sprite = sprite;
                Color = color;
            }
        }
    }
}
