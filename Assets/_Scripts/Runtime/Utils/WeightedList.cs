using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ribbons.RoguelikeGame
{
    public class WeightedList<TElement> : IEnumerable<TElement>
    {
        private Dictionary<TElement, FitnessInfo> _elements;

        public WeightedList(IEnumerable<KeyValuePair<TElement, float>> elementWeightPairs = null)
        {
            _elements = elementWeightPairs == null
                ? new Dictionary<TElement, FitnessInfo>()
                : elementWeightPairs.ToDictionary(el => el.Key, el => new FitnessInfo(el.Value));

            RecalculateProbabilities();
        }

        /// <summary>
        /// From a value [r] from [0.0f => 1.0f], return an element in the list with at least a probability of [r].
        /// </summary>
        /// <param name="r">A value from [0.0f => 1.0f] to compare with the chance for each element.</param>
        /// <returns>Matching element based on [r] < [probability]</returns>
        public TElement Get(float r)
        {
            foreach (var kvp in _elements)
                if (r < kvp.Value.Probability)
                    return kvp.Key;

            return _elements.Last().Key;
        }

        #region Change Weight
        public float GetWeight(TElement element)
        {
            return _elements[element].Fitness;
        }

        public void SetWeight(TElement element, float weight)
        {
            var temp = _elements[element];
            temp.Fitness = weight;
            _elements[element] = temp;

            RecalculateProbabilities();
        }
        #endregion

        #region Change List
        public bool Add(TElement element, float weight)
        {
            if (!_elements.TryAdd(element, new(weight)))
                return false;

            RecalculateProbabilities();
            return true;
        }

        public bool Remove(TElement element)
        {
            return _elements.Remove(element);
        }
        #endregion

        #region Utilitary Methods
        public bool Contains(TElement element)
        {
            return _elements.ContainsKey(element);
        }
        #endregion

        #region IEnumerable Stuff
        public IEnumerator<TElement> GetEnumerator() => _elements.Keys.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_elements.Keys).GetEnumerator();
        #endregion

        #region Fitness Proportionate Selection
        /* === EXPLANATION ===
         * In fitness proportionate selection a fitness level is used to
         * associate a probability of selection with each individual.
         * 
         * If F(i) is the fitness of individual 'i' in the population,
         * its probability P(i) of being selected is:
         * 
         *      P(i) = F(i) / N
         *  
         *  Where 'N' is the sum of all fitness levels in the population.
         * 
         * [Source: https://en.wikipedia.org/wiki/Fitness_proportionate_selection]
         * ---------------------------------------------------------------
         * To contextualize, when creating the list each element is assigned
         * a value (in any range), which we'll call a 'weight'.
         * 
         * Everytime the list changes (adding or removing elements),
         * as well as when it's created we iterate through it and,
         * for each element, calculate a probability of being chosen,
         * dividing the weight of the element by the sum of all weights.
         * 
         * It's also important to sort the list, after the probabilities
         * are calculated, so that when retrieving an element,
         * we check the list in order of less probable => more probable (0 => 1)
         */

        private void RecalculateProbabilities()
        {
            float lastProb = 0f;
            float fitSum = GetFitSum();

            var newDict = new Dictionary<TElement, FitnessInfo>();

            foreach (var kvp in _elements)
            {
                lastProb = GetProbability(kvp.Value.Fitness, fitSum, lastProb);
                newDict.Add(kvp.Key, new(kvp.Value.Fitness, lastProb));
            }

            _elements = new(newDict.OrderBy(e => e.Value.Probability));
        }

        private float GetProbability(float fitness, float fitSum, float lastProb = 0f)
        {
            return lastProb + (fitness / fitSum);
        }

        private float GetFitSum()
        {
            return _elements.Sum(e => e.Value.Fitness);
        }

        private struct FitnessInfo
        {
            public float Fitness;
            public float Probability;

            public FitnessInfo(float weight, float probability = 0f)
            {
                Fitness = weight;
                Probability = probability;
            }
        }
        #endregion
    }
}
