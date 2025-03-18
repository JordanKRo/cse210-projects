public class Deck {
    /// <summary>
    /// Contains a the list of cards in this deck. Can accept varying levels of variation.
    /// </summary>
    List<Card> cards;

    /// <summary>
    /// Creates a normal deck with 1 of every card fully defined.
    /// </summary>
    public Deck() {

    }
    /// <summary>
    /// Randomizes the order of the collection
    /// </summary>
    public void Shuffle(){

    }
    /// <summary>
    /// Remove the top card of the deck and return it.
    /// </summary>
    /// <returns></returns>
    public Card Draw(){
        throw new NotImplementedException("Cannot draw cards yet");
    }

    public override string ToString()
    {
        return base.ToString();
    }

    public static List<Card> GetFreshDeck(){
        List<Card> cards = new List<Card>();
        for(var suit = 0;suit < 4; suit ++){
            // Create the face cards
            cards.Add(new FaceCard((Suit) suit, Face.King));
            cards.Add(new FaceCard((Suit) suit, Face.Queen));
            cards.Add(new FaceCard((Suit) suit, Face.Jack));
            cards.Add(new FaceCard((Suit) suit, Face.Ace));
            // Add the numeric cards
            for (var val = 2;val <= 10;val++){
                cards.Add(new NumericCard((Suit) suit, val));
            }
            
        }

        return cards;

    }
}