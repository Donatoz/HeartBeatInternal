using System;
using Metozis.Cardistry.Internal.DataHandling.Databases;
using Metozis.Cardistry.Internal.GameFlow;
using Metozis.Cardistry.Internal.Management;
using Metozis.Cardistry.Internal.Meta.Core;
using Metozis.Cardistry.Internal.Meta.Descriptors;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Core.Entities.Cards
{
    public struct CreatureCardContext : ICardMainContext
    {
        public string CreatureId;
        
        public void InvokePlayContext(Card host)
        {
            GameUtils.SpawnUnit(UnitDatabase.GetUnit(CreatureId), Game.Current.AllyBoard);
        }
    }
}