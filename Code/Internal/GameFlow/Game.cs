using System;
using Metozis.Cardistry.Internal.Core.Entities;
using Metozis.Cardistry.Internal.Core.Interaction;
using Metozis.Cardistry.Internal.GameFlow.TurnMachines;
using Metozis.Cardistry.Internal.Management;

namespace Metozis.Cardistry.Internal.GameFlow
{
    [Serializable]
    public partial class Game
    {
        public GameCycle Cycle;
        public GameScheduler Scheduler;
        public static Game Current => SessionManager.Instance.CurrentGame;
        
        public Area AllyBoard;
        public Area EnemyBoard;
        public Area AllyHand;

        public Action OnGameStart;
        public Action OnGameEnd;
        
        public virtual void Start(Player localPlayer, Player opponent)
        {
            Cycle = new GameCycle(new HeartbeatTurnMachine(localPlayer, opponent));
            Cycle.Start(localPlayer, opponent);
            
            OnGameStart?.Invoke();
        }

        public virtual void End()
        {
            OnGameEnd?.Invoke();
        }
    }
}