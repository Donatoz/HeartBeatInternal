using System;
using Metozis.Cardistry.Internal.Core.Entities;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Modularity
{
    public class RotationModule : Module, IRuntimeModule
    {
        public delegate void RotationContextDelegate(Entity e, Quaternion rot);
        
        public Quaternion TargetRotation { get; set; }
        public RotationContextDelegate RotationContext;
        public RotationContextDelegate DefaultContext = delegate(Entity entity, Quaternion rot)
        {
            entity.transform.rotation = rot;
        };

        public RotationModule(Entity target, Quaternion initialRotation, bool enabled = true) : base(target, enabled)
        {
            RotationContext = DefaultContext;
            TargetRotation = initialRotation;
        }
        
        public virtual void Update()
        {
            TryInvokeContext(() =>
            {
                RotationContext.Invoke(target, TargetRotation);
            });
        }

        public void ResetContext()
        {
            RotationContext = DefaultContext;
        }
    }
}