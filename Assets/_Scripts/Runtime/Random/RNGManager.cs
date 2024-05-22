using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public static class RNGManager
    {
        private readonly static IRNGProvider RNGProvider = new SysRandRNGProvider();
        private static string _savedState = string.Empty;

        /// <returns>
        /// Current seed/state of the <see cref="RNGProvider"/>.
        /// </returns>
        public static string Seed
        {
            get => RNGProvider.GetSeed();
            set => RNGProvider.SetSeed(value);
        }

        /// <returns>
        /// Random value from <see cref="RNGProvider"/>
        /// </returns>
        public static float Value => RNGProvider.GetValue();

        /// <summary>
        /// Saves current <see cref="RNGProvider"/> state.
        /// To restore the saved state, use <see cref="RestoreState"/>
        /// Does not affect <seealso cref="FromSeed(string, bool)"/> and <seealso cref="UnseededValue"/>.
        /// </summary>
        /// <returns>Saved state.</returns>
        public static string SaveState()
        {
            return _savedState = Seed;
        }

        /// <summary>
        /// Restores a previously saved state of the <see cref="RNGProvider"/>
        /// To save the current state, use <see cref="SaveState"/>
        /// Does not affect <seealso cref="FromSeed(string, bool)"/> and <seealso cref="UnseededValue"/>.
        /// </summary>
        /// <returns>Whether or not there was a state to be restored.</returns>
        public static bool RestoreState()
        {
            if (string.IsNullOrEmpty(_savedState))
                return false;

            Seed = _savedState;
            _savedState = null;

            return true;
        }

        /// <returns>
        /// Random floating point value from [0.0f] [UpTo] [<paramref name="maxInclusive"/>].
        /// </returns>
        public static float UpTo(float maxInclusive) => RNGProvider.GetValue() * maxInclusive;

        /// <returns>
        /// Random floating point value from [0.0f] [UpTo] (but not equal) [<paramref name="maxExclusive"/>].
        /// </returns>
        public static int UpTo(int maxExclusive) => Mathf.FloorToInt(RNGProvider.GetValue() * maxExclusive);

        /// <returns>
        /// Random floating point value from [<paramref name="minInclusive"/>] [UpTo] [<paramref name="maxInclusive"/>].
        /// </returns>
        public static float Range(float minInclusive, float maxInclusive) => 
            minInclusive + (maxInclusive - minInclusive) * Value;

        /// <returns>
        /// Random floating point value from [<paramref name="minInclusive"/>] [UpTo] (but not equal) [<paramref name="maxExclusive"/>].
        /// </returns>
        public static int Range(int minInclusive, int maxExclusive) => 
            Mathf.FloorToInt(Range( (float)minInclusive, maxExclusive ));

        /// <returns>
        /// Random element of a given [<paramref name="list"/>].
        /// </returns>
        public static T InList<T>(IList<T> list) => list[Range(0, list.Count)];

        /// <returns>
        /// Random value from <see cref="RNGProvider"/> using the given seed then restore RNG state.
        /// </returns>
        /// <param name="keep">Keep used seed afterwards?</param>
        public static float FromSeed(string seed, bool keep = false)
        {
            string saveSeed = Seed;
            Seed = seed;

            float value = Value;
            Seed = keep ? seed : saveSeed;

            return value;
        }

        /// <returns>
        /// Random value from <see cref="RNGProvider"/> whitout advancing RNG state
        /// </returns>
        public static float UnseededValue()
        {
            return FromSeed(Seed);
        }

        /// <summary>
        /// Any changes to the <see cref="Seed"/> during the <paramref name="callback"/> execution are 
        /// to be ignored. The <see cref="RNGProvider"/> returns to it's previous state afterwards.
        /// </summary>
        public static void KeepSeed(Action callback)
        {
            var seed = Seed;
            callback?.Invoke();
            Seed = seed;
        }

        /// <returns>
        /// A <see cref="Vector2"/> whose x and y componnents are both random numbers, either [-1], [0] or [1].
        /// </returns>
        public static Vector2 Direction()
        {
            return new()
            {
                x = Range(-1, 2),
                y = Range(-1, 2),
            };
        }
    }
}
