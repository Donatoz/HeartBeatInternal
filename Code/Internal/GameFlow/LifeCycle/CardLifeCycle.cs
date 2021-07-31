using System;
using System.Collections.Generic;
using System.Linq;
using Bolt;
using Ludiq;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Interaction;
using Metozis.Cardistry.Internal.Core.Modularity;
using Metozis.Cardistry.Internal.Core.Reactive;
using Metozis.Cardistry.Internal.Core.Utils;
using Metozis.Cardistry.Internal.Management;
using UnityEngine;

using Unit = Metozis.Cardistry.Internal.Core.Entities.Unit;

namespace Metozis.Cardistry.Internal.GameFlow.LifeCycle
{
    public class CardLifeCycle : LifeCycle
    {
        public Action OnLive;
        public Action OnDeath;
        public Action OnPlayBegin;
        public Action OnPlayEnd;

        private Vector3 cardOriginalPosition;

        public CardLifeCycle(Card target, GraphReference cardLogic) : base(target, cardLogic)
        {
            OnLive += delegate
            {
                LogicContext.CallLogicEvent(cardLogic, "OnLive");
            };
            target.OnInitialized += OnLive;
            
            OnPlayBegin += delegate
            {
                LogicContext.CallLogicEvent(cardLogic, "OnPlayBegin");
            };
            OnPlayEnd += delegate
            {
                LogicContext.CallLogicEvent(cardLogic, "OnPlayEnd");
            };
            OnDeath += delegate
            {
                LogicContext.CallLogicEvent(cardLogic, "OnDeath");
            };
            
            target.GetModule<DragModule>().Handler.OnDragStart += delegate
            {
                cardOriginalPosition = target.transform.position - Vector3.up;
                target.GetModule<LookAtModule>().RotationContext = (entity, rot) =>
                {
                    entity.transform.rotation = Quaternion.Lerp(entity.transform.rotation, Quaternion.Euler(-90, 180, 0), Time.deltaTime * 10);
                };
            };
            
            target.GetModule<DragModule>().Handler.OnDragEnd += delegate
            {
                var currentEntity = ManagersRoot.Instance.Get<InputManager>().State.CursorCollisionHitInfo.collider;
                var area = currentEntity.GetComponent<Area>();
                if (currentEntity != null 
                    && area != null 
                    && area.Owner == target.Controller 
                    && area.Validator.TypesTuple.GetTypes().Contains(typeof(Unit))
                    && !area.Contains(target)
                )
                {
                    Play(delegate { target.MetaDescriptor.MainContext.InvokePlayContext(target); });
                }
                else
                {
                    target.GetModule<FollowModule>().MakeOffset(cardOriginalPosition);
                }

                target.GetModule<LookAtModule>().ResetContext();
            };
        }

        public virtual void Play(Action mainContext)
        {
            OnPlayBegin.Invoke();
            
            mainContext.Invoke();
            
            OnPlayEnd.Invoke();
            
            OnDeath.Invoke();
        }

        public virtual void Discard()
        {
            OnDeath.Invoke();
        }
    }
}