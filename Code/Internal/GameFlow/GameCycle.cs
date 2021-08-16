using System;
using Metozis.Cardistry.Internal.GameFlow.Branching;

namespace Metozis.Cardistry.Internal.GameFlow
{
    public class GameCycle
    {
        public ITurnMachine TurnMachine;
        public FlowTracker Tracker;
        
        private Player playerOne;
        private Player playerTwo;

        public GameCycle(ITurnMachine turnMachine)
        {
            TurnMachine = turnMachine;
            Tracker = new FlowTracker(new AsyncFlowBranch(null));
        }
        
        public void Start(Player p1, Player p2)
        {
            playerOne = p1;
            playerTwo = p2;
            
            playerOne.Opponent = playerTwo;
            playerTwo.Opponent = playerOne;
            playerOne.IsActualPlayer = true;
        }
    }
}