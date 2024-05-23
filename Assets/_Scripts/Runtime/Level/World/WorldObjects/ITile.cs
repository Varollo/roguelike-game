using System.Collections.Generic;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public interface ITile : IEnumerable<ITileComponent>
    {
        TComponent GetComponent<TComponent>() where TComponent : ITileComponent;
        void OnDestroy();
        void OnPositionMove(Vector2Int newPos);
    }
}
