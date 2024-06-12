using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Ribbons.RoguelikeGame
{
    public class InputManager : IManager, IUpdateListener
    {        
        private readonly bool _swipeOnlyOnRelease = false;
        private readonly float _swipeThreshold = 20f;

        private Dictionary<Type, InputController> _controllers;

        private Dictionary<Type, InputController> Controllers => _controllers ??=
            MakeControllers().ToDictionary(ctrl => ctrl.GetType(), ctrl => ctrl);

        public static TController GetController<TController>() where TController : InputController
        {
            return ManagerMaster.GetManager<InputManager>().Controllers[typeof(TController)] as TController;
        }

        private List<InputController> MakeControllers() => new()
        {
            new TouchInputController(),
            new SwipeInputController(_swipeThreshold, _swipeOnlyOnRelease),
        };

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

        #region Messager Callbacks
        public void OnUpdate()
        {
            foreach (Touch touch in Input.touches)
                HandleTouchPhase(touch);
        }

        public void OnInit() { }
        public void OnDestroy() { }
        #endregion
    }
}
