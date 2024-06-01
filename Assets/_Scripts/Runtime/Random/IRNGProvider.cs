namespace Ribbons.RoguelikeGame
{
    public interface IRNGProvider
    {
        /// <returns>
        /// Random float in the interval: [0.0f -> 1.0f]
        /// </returns>
        float GetValue();

        /// <summary>
        /// To set the current seed, see <seealso cref="SetSeed(string)"/>
        /// </summary>
        /// <returns>
        /// Current state of the RNG as a string.
        /// </returns>
        string GetSeed();

        /// <summary>
        /// Sets the state of the RNG to a string. 
        /// To read the current seed, see <seealso cref="GetSeed"/>
        /// </summary>
        void SetSeed(string seed);

        /// <summary>
        /// Converts a string to an int using the <see cref="object.GetHashCode"/> method.
        /// </summary>
        protected static int SeedStr2Int(string seed) => seed.GetHashCode();
    }
}
