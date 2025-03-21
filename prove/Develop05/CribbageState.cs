using System.Security;

/// <summary>
/// Holds the current state of all of the cards at a given time. This is used for the known state of the game.
/// </summary>
public class CribbageState {
    List<Hand> player_hands = new List<Hand>();
    Collection crib = new Collection();
    Deck original_deck = new Deck();

    public CribbageState(Deck origin, int player_count){
        original_deck = origin;
        for (int i = 0; i < player_count; i++) {
            player_hands.Add(new Hand());
        }
    }

    public CribbageState(){
        original_deck = new Deck(Deck.GetFreshDeck());
    }

    public void SetHands(List<Hand> hands){
        player_hands = hands;
    }

    // some way to get the probabilities
    /// <summary>
    /// Logically tell the interpreter that each player receives a random set of n cards.
    /// </summary>
    public void Deal(int amount){

    }
}