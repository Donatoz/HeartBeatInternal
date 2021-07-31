using System;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Interaction;
using Metozis.Cardistry.Internal.Core.Utils;
using Metozis.Cardistry.Internal.GameFlow;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metozis.Cardistry.Internal.Management
{
    public class SessionManager : MonoBehaviour
    {
        public static SessionManager Instance => ManagersRoot.Instance.Get<SessionManager>();
        
        public Game CurrentGame;

        public RuntimeContext Runtime;

        public Player PlayerOne;
        public Player PlayerTwo;
        public Player ActualPlayer => PlayerOne.IsActualPlayer ? PlayerOne : PlayerTwo;
        
        public Area AllyBoard;
        public Area AllyHand;
        public Area EnemyBoard;

        private void Awake()
        {
            Runtime = new RuntimeContext();
        }

        private void Start()
        {
            CurrentGame = new Game
            {
                AllyBoard = AllyBoard,
                EnemyBoard = EnemyBoard,
                AllyHand = AllyHand
            };
        }
    }
}