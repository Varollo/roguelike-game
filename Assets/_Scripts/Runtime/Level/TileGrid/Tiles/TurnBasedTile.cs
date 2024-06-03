using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class TurnBasedTile : TransformTile
    {
        protected TurnBasedTile(Transform tileTransform, params ITileComponent[] components) : base(tileTransform, components)
        {
        }

        protected virtual TileTurnListenerComponent TurnListenerComponent { get; set; }

        protected virtual TileTurnListenerComponent CreateTurnListenerComponent()
        {
            return new(this, OnTurnAction, OnTurnStart, OnTurnEnd);
        }

        protected override List<ITileComponent> GetDefaultComponents() => new(base.GetDefaultComponents())
        {
            (TurnListenerComponent = CreateTurnListenerComponent()),
        };

        protected virtual void OnTurnStart(ulong turnCount) { }
        protected virtual void OnTurnEnd(ulong turnCount) { }
        protected virtual void OnTurnAction(ulong turnCount) { }
    }
}
