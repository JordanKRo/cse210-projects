public class Fraction{
    private int _numerator;
    private int _denominator;

    public Fraction(){
        _numerator = 1;
        _denominator = 1;
    }

    public Fraction(int numerator){
        _numerator = numerator;
        _denominator = 1;
    }

    public Fraction(int numerator, int denominator) {
        _numerator = numerator;
        _denominator = denominator;
    }

    public int GetNumerator() {
        return _numerator;
    }

    public int GetDenominator() {
        return _denominator;
    }

    public void SetNumerator(int numerator) {
        _numerator = numerator;
    }

    public void SetDenominator(int denominator) {
        _denominator = denominator;
    }

    public string GetString() {
        return $"{_numerator}/{_denominator}";
    }

    public double GetDecimal() {
        return (double) _numerator / _denominator;
    }

    // overrides ToString so it automatically calls GetString() when casting
    public override string ToString()
    {
        return GetString();
    }

    // Compares if 2 fractions have the same decimal value
    public override bool Equals(object obj)
    {
        
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        
        return GetDecimal() == ((Fraction) obj).GetDecimal();
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_numerator, _denominator);
    }
}