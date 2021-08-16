using System;
using System.Collections.Generic;
using System.Linq;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Interaction;
using Metozis.Cardistry.Internal.Core.Reactive;
using Metozis.Cardistry.Internal.DataHandling.Databases;
using Metozis.Cardistry.Internal.GameFlow;
using Metozis.Cardistry.Internal.VFX;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Management
{
    public class DebugManager : SerializedMonoBehaviour
    {
        public Card TestCard;
        public Area TestArea;
        
        private SessionManager session;
        
        private void Start()
        {
            TestArea.Add(TestCard);
            GameUtils.SpawnUnit(UnitDatabase.GetUnit("1"), Game.Current.EnemyBoard);
            GameUtils.SpawnCard(CardDatabase.GetCard("1"), Game.Current.AllyHand);
            GameUtils.SpawnCard(CardDatabase.GetCard("1"), Game.Current.AllyHand);
        }

        public void DebugSkill()
        {
            ((Unit)Game.Current.AllyBoard.First()).CastSkill(0);
        }
    }
}