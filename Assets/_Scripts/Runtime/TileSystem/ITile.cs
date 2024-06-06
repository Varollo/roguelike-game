using UnityEngine;

namespace Ribbons.RoguelikeGame.TileSystem
{
    public interface ITile
    {
        void OnInit();
        void OnKill();
    }
}
