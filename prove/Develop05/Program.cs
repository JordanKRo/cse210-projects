using System;
using System.Buffers.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
using ToolBox;

class Program
{
    static void Main(string[] args)
    {   
        List<Card> cards = new List<Card>{
            new NumericCard(Suit.Diamonds, 2),
            new NumericCard(Suit.Diamonds, 3),
            new NumericCard(Suit.Diamonds, 4),
            new NumericCard(Suit.Diamonds, 5),
            new NumericCard(Suit.Diamonds, 6),
            new NumericCard(Suit.Diamonds, 7),
        };

        Deck origin = new Deck(cards);
        CribbageState state = new CribbageState(origin, 2);

        state.Deal(2);
        // tell state what I have
        // some kind of state.update my hand
    }


}