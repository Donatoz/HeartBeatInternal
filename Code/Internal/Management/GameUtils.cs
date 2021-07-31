using System;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Interaction;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Descriptors;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Management
{
    public static class GameUtils
    {
        public static Unit SpawnUnit(UnitMetaDescriptor descriptor, Area area, bool sameOwner = true)
        {
            var unit = SessionManager.Instance.Runtime.UnitFactory.Create(descriptor, new CreationPass
                {
                    PrefabPath = "Unit",
                    PrePass = delegate(Entity entity)
                    {
                        (entity as ActingEntity).Controller = sameOwner ? area.Owner : area.Owner.Opponent;
                    }
                });
            area.Add(unit);
            unit.OnDeath += delegate
            {
                area.Remove(unit);
            };
            return unit;
        }

        public static Card SpawnCard(CardMetaDescriptor descriptor, Area area, bool sameOwner = true)
        {
            var card = SessionManager.Instance.Runtime.CardFactory.Create(descriptor, new CreationPass
            {
                PrefabPath = "Card",
                PrePass = delegate(Entity entity)
                {
                    (entity as ActingEntity).Controller = sameOwner ? area.Owner : area.Owner.Opponent;
                    (entity as Card).CardLogicMacro = descriptor.CardLogicMacro;
                }
            });
            area.Add(card);
            return card;
        }
    }
}