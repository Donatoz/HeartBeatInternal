using System;
using System.Reflection;
using Ludiq;
using Metozis.Cardistry.Internal.Core.Construct;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Orders;
using Metozis.Cardistry.Internal.Core.Utils;

namespace Metozis.Cardistry.Internal.GameFlow.LifeCycle
{
    public class UnitLifeCycle : LifeCycle
    {
        public Action OnLive;
        public Action OnDeath;
        public Action OnAttackBegin;
        public Action OnAttackEnd;
        public Action OnAttacked;

        private Unit unitTarget;

        public UnitLifeCycle(Unit target, GraphReference unitLogic, bool enabled = true) : base(target, unitLogic)
        {
            unitTarget = target;
            OnLive += delegate
            {
                LogicContext.CallLogicEvent(unitLogic, "OnLive");
            };
            OnDeath += delegate
            {
                LogicContext.CallLogicEvent(unitLogic, "OnDeath");
            };
            OnAttackBegin += delegate
            {
                LogicContext.CallLogicEvent(unitLogic, "OnAttackBegin");
            };
            OnAttackEnd += delegate
            {
                LogicContext.CallLogicEvent(unitLogic, "OnAttackEnd");
            };
            OnAttacked += delegate
            {
                LogicContext.CallLogicEvent(unitLogic, "OnAttacked");
            };
        }
    }
}