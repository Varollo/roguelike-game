using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public abstract class InputController
    {
        public virtual void TouchBegan(Touch touch) { }
        public virtual void TouchEnded(Touch touch) { }
        public virtual void TouchMoved(Touch touch) { }
    }
}