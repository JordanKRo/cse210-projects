public class NumericCard : Card{
    private int value;

    public NumericCard(Suit suit, int num) : base(suit){
        value = num;
    }
}