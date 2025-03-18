using System.Xml;

public class Card {
    /// <summary>
    /// The unique number given to this card in context with it's deck
    /// </summary>
    protected int UID;
    protected Suit suit;

    /// <summary>
    /// Accepts nulls as unknown
    /// </summary>
    public Card(){

    }

    public Card(Suit suit){
        this.suit = suit;
    }
}

public enum Suit{
    Hearts,
    Clubs,
    Spades,
    Diamonds
}