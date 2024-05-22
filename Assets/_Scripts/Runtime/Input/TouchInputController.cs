using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class TouchInputController : InputController
    {
        public delegate void TouchDelegate(Touch touch);

        public event TouchDelegate OnTouchBegan;
        public event TouchDelegate OnTouchMoved;
        public event TouchDelegate OnTouchEnded;

        public override void TouchBegan(Touch touch)
        {
            OnTouchBegan?.Invoke(touch);
        }

        public override void TouchMoved(Touch touch)
        {
            OnTouchMoved?.Invoke(touch);
        }

        public override void TouchEnded(Touch touch)
        {
            OnTouchEnded?.Invoke(touch);
        }
    }
}
