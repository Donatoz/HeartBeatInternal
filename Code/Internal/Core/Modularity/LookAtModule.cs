using System;
using Metozis.Cardistry.Internal.Core.Entities;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Modularity
{
    public class LookAtModule : RotationModule
    {
        public Transform LookAtTarget;
        public Vector3 LookAxis = Vector3.up;

        public LookAtModule(Entity target, Transform lookAtTarget, bool enabled = true) : base(target, Quaternion.identity, enabled)
        {
            LookAtTarget = lookAtTarget;
        }

        public override void Update()
        {
            TargetRotation = Quaternion.LookRotation(LookAtTarget.transform.position - target.transform.position, LookAxis);
            base.Update();
        }
    }
}