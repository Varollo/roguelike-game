using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    /// <summary>
    /// Swipe logic based on this code by <see href="alialacan"/> on
    /// <see href="https://gist.github.com/alialacan/1eddcd107f4a48a46dea17695ca151f2">GitHub</see>
    /// Adpted by <see href="Varollo"/>, for refactoring and performance reasons.
    /// </summary>
    public class SwipeInputController : InputController
    {
        public delegate void SwipeDelegate(Touch touch, Vector2 dir);
        public event SwipeDelegate OnSwipe;

        private Vector2 _lastSwipe;
        private Vector2 _fingerUpPos;
        private Vector2 _fingerDownPos;
        
        public SwipeInputController(float swipeThreshold, bool swipeOnlyOnRelease = false)
        {
            SwipeThreshold = swipeThreshold;
            SwipeOnlyOnRelease = swipeOnlyOnRelease;
        }

        private bool SwipeOnlyOnRelease { get; set; }
        private float SwipeThreshold { get; set; }

        public override void TouchBegan(Touch touch)
        {
            _fingerDownPos = touch.position;
            _fingerUpPos = touch.position;
        }

        public override void TouchEnded(Touch touch)
        {
            _fingerDownPos = touch.position;

            if (SwipeOnlyOnRelease)
                DetectSwipe(touch);

            _lastSwipe = Vector2.zero;
        }

        public override void TouchMoved(Touch touch)
        {
            _fingerDownPos = touch.position;

            if (!SwipeOnlyOnRelease)
                DetectSwipe(touch);
        }

        private void HandleSwipe(Touch touch, Vector2 dir)
        {
            OnSwipe?.Invoke(touch, dir);
            _fingerUpPos = _fingerDownPos = touch.position;
            _lastSwipe = dir;
        }

        private void DetectSwipe(Touch touch)
        {
            Vector2 swipeDelta = GetFingerPosDelta();
            Vector2 newSwipe = Vector2.zero;

            if (IsVerticalSwipe(swipeDelta))
                newSwipe.y = Mathf.Sign(_fingerDownPos.y - _fingerUpPos.y);

            else if (IsHorizontalSwipe(swipeDelta)) // to alow diagnoals, just take out the 'else'
                newSwipe.x = Mathf.Sign(_fingerDownPos.x - _fingerUpPos.x);

            if (newSwipe != Vector2.zero && _lastSwipe == Vector2.zero)
                HandleSwipe(touch, newSwipe);
        }

        private bool IsVerticalSwipe(Vector2 swipeDelta)
        {
            return swipeDelta.y > SwipeThreshold;
                //&& swipeDelta.y > swipeDelta.x;
        }

        private bool IsHorizontalSwipe(Vector2 swipeDelta)
        {
            return swipeDelta.x > SwipeThreshold;
                //&& swipeDelta.x > swipeDelta.y;
        }

        private Vector2 GetFingerPosDelta() => new()
        {
            x = Mathf.Abs(_fingerDownPos.x - _fingerUpPos.x),
            y = Mathf.Abs(_fingerDownPos.y - _fingerUpPos.y)
        };
    }
}
