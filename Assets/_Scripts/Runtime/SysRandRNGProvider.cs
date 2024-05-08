using Random = System.Random;

namespace Ribbons.RoguelikeGame
{
    public class SysRandRNGProvider : IRNGProvider
    {
        private Random _rng;
        private string _seedString;

        public SysRandRNGProvider() : this(System.Guid.NewGuid().ToString()) { }
        public SysRandRNGProvider(string seed) => SetSeed(seed);

        public string GetSeed() => _seedString;
        public float GetValue() => (float)_rng.NextDouble();
        public void SetSeed(string seed) => _rng = new(IRNGProvider.SeedStr2Int(_seedString = seed));
    }
}
