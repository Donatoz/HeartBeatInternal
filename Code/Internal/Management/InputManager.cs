using System;
using System.Collections.Generic;
using System.Linq;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Interaction;
using Metozis.Cardistry.Internal.GameFlow;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Metozis.Cardistry.Internal.Management
{
    public class InputManager : MonoBehaviour
    {
        public static InputManager Instance => ManagersRoot.Instance.Get<InputManager>();
        
        public struct InputState
        {
            /// <summary>
            /// Two-dimensional cursor transform.
            /// </summary>
            public Transform CursorTransform;
            /// <summary>
            /// Three-dimensional cursor transform.
            /// </summary>
            public Transform CursorCollisionTransform;

            public Entity PointedEntity;
            public RaycastHit CursorCollisionHitInfo;
        }
        
        public InputState State;
        public PlayerInput MainPlayerInput;

        private Dictionary<string, List<Action<InputAction.CallbackContext>>> inputActions =
            new Dictionary<string, List<Action<InputAction.CallbackContext>>>();

        private bool cardsStackedUp;

        private void Awake()
        {
            State = new InputState
            {
                CursorTransform = new GameObject("Mouse cursor").transform,
                CursorCollisionTransform = new GameObject("Mouse collision cursor").transform
            };
            MainPlayerInput.onActionTriggered += HandleTriggeredAction;
        }

        private void HandleTriggeredAction(InputAction.CallbackContext ctx)
        {
            if (inputActions.ContainsKey(ctx.action.name))
            {
                inputActions[ctx.action.name].ForEach(inputCtx => inputCtx.Invoke(ctx));
            }
        }

        private void Start()
        {
            RegisterInputAction("CardsStackUp", ctx =>
            {
                cardsStackedUp = !cardsStackedUp;
                ((AxisAreaPlacer) Game.Current.AllyHand.AreaPlacer).ObjectWidth = cardsStackedUp ? 0 : 3;
            });
        }

        private void Update()
        {
            Vector3 mousePos = Mouse.current.position.ReadValue();
            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            var mask = LayerMask.GetMask("Default");
            
            if (Physics.Raycast(ray, out var hit, 100, mask))
            {
                State.CursorCollisionTransform.position = hit.point;
                State.CursorCollisionHitInfo = hit;
            }

            if (Physics.Raycast(ray, out var rawHit))
            {
                State.PointedEntity = rawHit.collider.GetComponent<Entity>();
            }
            
            mousePos.z = Camera.main.nearClipPlane * 2;
            State.CursorTransform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

        public void RegisterInputAction(string actionName, Action<InputAction.CallbackContext> ctx)
        {
            if (inputActions.ContainsKey(actionName))
            {
                inputActions[actionName].Add(ctx);
            }
            else
            {
                inputActions[actionName] = new List<Action<InputAction.CallbackContext>> {ctx};
            }
        }
    }
}