using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardsLab.Models
{
    public class Card
    {
        public string image { get; set; }
        public string value { get; set; }
        public string suit { get; set; }
        public override string ToString()
        {
            return $"{value} of {suit}";
        }

    }
    public class CardApiResponse
    {
        public string deck_id { get; set; }
        public int remaining { get; set; }
        public List<Card> cards { get; set; }
    }
    public class DeckID
    {
        public static string deck_id { get; set; }
    }
    public class DealerHand
    {
        public List<Card> cards { get; set; }
    }
    public class PlayerHand
    {
        public List<Card> cards { get; set; }
    }
    public class BlackjackGame
    {
        public static DealerHand dealer { get; set; }
        public static PlayerHand player { get; set; }
        public static bool playerTurn { get; set; }
        public static bool dealerTurn { get; set; }
        public static string deck_id { get; set; }

    }
}
