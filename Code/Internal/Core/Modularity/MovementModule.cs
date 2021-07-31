using System;
using Metozis.Cardistry.Internal.Core.Entities;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Modularity
{
    public class MovementModule : Module
    {
        public bool EnableMovement { get; set; }

        public Vector3 MovementDelta
        {
            get
            {
                return target.transform.position - lastPosition;
            }
        }


        protected Vector3 lastPosition;
        
        public Action<Entity, Vector3> MovementContext = delegate(Entity target, Vector3 pos)
        {
            target.transform.position = pos;
        };
        
        public MovementModule(Entity target, Action<Entity, Vector3> movementCtx = null) : base(target)
        {
            if (movementCtx != null)
            {
                MovementContext = movementCtx;
            }

            lastPosition = target.transform.position;
        }

        public virtual void Move(Vector3 position, bool force = false)
        {
            TryInvokeContext(delegate
            {
                if (!force)
                {
                    if (EnableMovement)
                    {
                        lastPosition = target.transform.position;
                        MovementContext.Invoke(target, position);
                    }
                }
                else
                {
                    lastPosition = target.transform.position;
                    MovementContext.Invoke(target, position);
                }
            });
        }
    }
}