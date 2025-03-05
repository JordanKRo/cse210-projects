using System;

class Program
{
    static void Main(string[] args)
    {
        Shape shape = new Circle("RED", 8);
        Console.WriteLine(shape.GetArea());
    }
}