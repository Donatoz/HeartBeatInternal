using System;

namespace Metozis.Cardistry.Internal.GameFlow
{
    public interface ITurnMachine
    {
        Player ActingPlayer { get; }
        Player NextPlayer { get; }
        void EndTurn();
        
        Action<Player> OnPlayerTurnStart { get; set; }
        Action<Player> OnPlayerTurnEnd { get; set; }
    }
}