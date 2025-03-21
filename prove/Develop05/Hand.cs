public class Hand : Collection{
    int holder;
    public Hand() : base() {

    }

    public Hand(int owner){
        this.holder = owner;
    }

    public Hand(List<Card> cards, int owner) : base(cards){
        this.holder = owner;
    }
}