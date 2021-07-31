using System;
using Metozis.Cardistry.Internal.Core.Entities;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Modularity
{
    public class FollowModule : MovementModule, IRuntimeModule
    {
        private Vector3 lastTargetPosition;
        
        private Transform followTarget;
        
        public FollowModule(Entity target, Action<Entity, Vector3> movementCtx = null) : base(target, movementCtx)
        {
            followTarget = new GameObject("Follow Anchor").transform;
            lastTargetPosition = target.transform.position;
            followTarget.transform.position = target.transform.position;
            target.OnDestroyed += delegate
            {
                if (followTarget != null)
                {
                    MonoBehaviour.Destroy(followTarget.gameObject);
                }
            };
        }

        public void MakeOffset(Vector3 newPosition)
        {
            followTarget.transform.position = newPosition;
        }

        public void Update()
        {
            TryInvokeContext(delegate
            {
                if (EnableMovement)
                {
                    lastTargetPosition = followTarget.position;
                }
                Move(lastTargetPosition, true);
            });
        }
    }
}