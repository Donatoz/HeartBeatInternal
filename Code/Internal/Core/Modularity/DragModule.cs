using System;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Management;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Modularity
{
    public sealed class DragHandler : MonoBehaviour
    {
        public Action OnDragStart;
        public Action OnDragEnd;

        private void OnMouseDown()
        {
            OnDragStart?.Invoke();
        }

        private void OnMouseUp()
        {
            OnDragEnd?.Invoke();
        }
    }
    
    public class DragModule : Module, IRuntimeModule
    {
        public bool IsDragged { get; private set; }
        public DragHandler Handler;

        public Action<Vector3> OnDrag;
        
        public DragModule(Entity target, bool enabled = true) : base(target, enabled)
        {
            if (target.GetComponent<DragHandler>() == null)
            {
                target.gameObject.AddComponent<DragHandler>();
            }

            Handler = target.GetComponent<DragHandler>();
            Handler.OnDragStart += BeginDrag;
            Handler.OnDragEnd += EndDrag;
        }

        private void BeginDrag()
        {
            IsDragged = true;
        }

        private void EndDrag()
        {
            IsDragged = false;
        }

        public virtual void Update()
        {
            if(Enabled) {
                if (IsDragged)
                {
                    OnDrag?.Invoke(ManagersRoot.Instance.Get<InputManager>().State.CursorCollisionTransform.position);
                }
            }
        }
    }
}