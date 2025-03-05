public class Square : Shape
{
    private double _size = 0;
    public Square(string color, double size) : base(color){
        _size = size;
    }

    public override double GetArea()
    {
        return Math.Pow(_size, 2);
    }
}