using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class InputManager : UnitySingleton<InputManager>
    {
        [Header("Swipe")]
        [SerializeField] private bool swipeOnlyOnRelease = false;
        [SerializeField] private float swipeThreshold = 20f;

        private Dictionary<Type, InputController> _controllers;

        private Dictionary<Type, InputController> Controllers => _controllers ??=
            MakeControllers().ToDictionary(ctrl => ctrl.GetType(), ctrl => ctrl);

        public static TController GetController<TController>() where TController : InputController
        {
            return Instance.Controllers[typeof(TController)] as TController;
        }

        private List<InputController> MakeControllers() => new()
        {
            new TouchInputController(),
            new SwipeInputController(swipeThreshold, swipeOnlyOnRelease),
        };

        private void Update()
        {
            foreach (Touch touch in Input.touches)
                HandleTouchPhase(touch);
        }

        private void HandleTouchPhase(Touch touch)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnAllControllers(
                        ctrl => ctrl.TouchBegan(touch)
                    );
                    break;

                case TouchPhase.Moved:
                    OnAllControllers(
                        ctrl => ctrl.TouchMoved(touch)
                    );
                    break;

                case TouchPhase.Ended:
                    OnAllControllers(
                        ctrl => ctrl.TouchEnded(touch)
                    );
                    break;
            }
        }

        public void OnAllControllers(Action<InputController> onController)
        {
            foreach (InputController controller in Controllers.Values)
                onController?.Invoke(controller);
        }
    }
}
