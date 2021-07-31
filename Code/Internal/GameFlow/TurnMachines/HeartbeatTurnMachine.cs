using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Metozis.Cardistry.Internal.GameFlow.TurnMachines
{
    public class HeartbeatTurnMachine : ITurnMachine
    {
        public Player ActingPlayer { get; private set; }

        public Player NextPlayer => turnQueue.Peek();
        
        public Action<Player> OnPlayerTurnStart { get; set; }
        public Action<Player> OnPlayerTurnEnd { get; set; }
        
        private readonly Player playerOne;
        private readonly Player playerTwo;
        private readonly Queue<Player> turnQueue;

        public HeartbeatTurnMachine(Player p1, Player p2, bool randomStart = true)
        {
            playerOne = p1;
            playerTwo = p2;
            turnQueue = new Queue<Player>();
            ActingPlayer = randomStart ? (Random.Range(0, 2) == 1 ? playerOne : playerTwo) : playerOne;
            turnQueue.Enqueue(ActingPlayer.Opponent);
        }
        
        public void EndTurn()
        {
            ActingPlayer = turnQueue.Dequeue();
            turnQueue.Enqueue(ActingPlayer.Opponent);
        }
    }
}