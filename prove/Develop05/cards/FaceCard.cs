public class FaceCard : Card {
    private Face face;

    public FaceCard(Suit suit, Face face) : base(suit){
        this.face = face;
    }
}

public enum Face {
    King,
    Queen,
    Jack,
    Joker,
    Ace,
}