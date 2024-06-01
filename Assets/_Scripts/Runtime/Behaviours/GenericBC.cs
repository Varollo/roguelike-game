using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class GenericBC : BehaviourController
    {
        private readonly Dictionary<string, Type> _typeNameDictionary = new()
        {
            { nameof(MoveRandDirBehaviour), typeof(MoveRandDirBehaviour) }
        };

        [SerializeField] private string behaviourTypeName;

        protected override TurnBasedBehaviour GetNewBehaviour()
        {
            if (!_typeNameDictionary.TryGetValue(behaviourTypeName, out Type t))
            {
                try
                {
                    StringBuilder typeNameBuilder = new(typeof(TurnBasedBehaviour).Namespace);
                    typeNameBuilder.Append('.');
                    typeNameBuilder.Append(behaviourTypeName);

                    t = Type.GetType(typeNameBuilder.ToString(), true, true);
                }
                catch (Exception e)
                {
                    Debug.LogError($"[GenericBehaviourController Error]: Could not create Behaviour instance from type name \"{behaviourTypeName}\".\n{e}");
                    throw;
                }
            }

            return Activator.CreateInstance(t) as TurnBasedBehaviour;
        }
    }
}
