using System;

namespace Metozis.Cardistry.Internal.GameFlow
{
    public class GameCycle
    {
        public GameState CurrentState { get; private set; }

        public ITurnMachine TurnMachine;

        private Player playerOne;
        private Player playerTwo;

        public GameCycle(ITurnMachine turnMachine)
        {
            TurnMachine = turnMachine;
        }
        
        public void Start(Player p1, Player p2)
        {
            playerOne = p1;
            playerTwo = p2;
            
            playerOne.Opponent = playerTwo;
            playerTwo.Opponent = playerOne;
            playerOne.IsActualPlayer = true;

            CurrentState = new GameState();
        }
    }
}