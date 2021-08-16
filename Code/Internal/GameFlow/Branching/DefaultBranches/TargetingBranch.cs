using System;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Interaction;
using Metozis.Cardistry.Internal.Core.Reactive;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.VFX;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Metozis.Cardistry.Internal.GameFlow.Branching.DefaultBranches
{
    public class TargetingBranch : GenericFlowBranch
    {
        public Action<ISelectable> OnTargetSelected;

        private bool selected;
        private readonly ActingEntity caster;
        private VFXEntity targetingVfx;
        
        public TargetingBranch(ActingEntity caster, FlowBranch parent) : base(parent)
        {
            this.caster = caster;
            
            OnTargetSelected += delegate
            {
                selected = true;
                Object.Destroy(targetingVfx);
                ReturnFlow();
            };
            
            OnFlowStart += delegate
            {
                SelectionManager.Instance.OnObjectSelected += OnTargetSelected;
                SpawnTargetingFx();
            };
            
            OnFlowEnd += delegate
            {
                SelectionManager.Instance.OnObjectSelected -= OnTargetSelected;
            };
        }

        private void SpawnTargetingFx()
        {
            var effect = Object.Instantiate(VFXManager.Instance.Cache["TargetingEffect"], caster.transform.position, Quaternion.identity);
            VisualScheduler.Instance.Schedule(new VFXScheduleContext 
            {
                Entity = effect
            });

            var lr = effect.GetComponent<LineRenderer>();
            
            ReactiveUtils.Interval(0.03f, () =>
            {
                lr.SetPosition(1, InputManager.Instance.State.CursorCollisionHitInfo.point);
            }, () => selected);
        }
    }
}