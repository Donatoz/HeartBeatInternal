using Metozis.Cardistry.Internal.Core.Interaction;
using Metozis.Cardistry.Internal.GameFlow.TurnMachines;
using Metozis.Cardistry.Internal.Management;

namespace Metozis.Cardistry.Internal.GameFlow
{
    public class Game
    {
        public GameCycle Cycle;
        public static Game Current => ManagersRoot.Instance.Get<SessionManager>().CurrentGame;
        
        public Area AllyBoard;
        public Area EnemyBoard;
        public Area AllyHand;
        public Area EnemyHand;

        public void Start(Player localPlayer, Player opponent)
        {
            Cycle = new GameCycle(new HeartbeatTurnMachine(localPlayer, opponent));
            Cycle.Start(localPlayer, opponent);
        }
    }
}