public class Rectangle : Shape{
    private double _width;
    private double _length;

    public Rectangle(string color, double length, double width) : base(color){
        _length = length;
        _width = width;
    }
    public override double GetArea()
    {
        return _length * _width;
    }
}