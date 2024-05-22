using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public interface IWorldObject : IEnumerable<IObjectComponent>
    {
        TComponent GetComponent<TComponent>() where TComponent : IObjectComponent;
        void OnKill();
        void OnPositionChange(Vector2Int newPos);
    }
}
