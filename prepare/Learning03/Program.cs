using System;

class Program
{
    static void Main(string[] args)
    {
        Fraction first = new Fraction();
        Fraction second = new Fraction(5);
        Fraction third = new Fraction(3,4);
        Fraction fourth = new Fraction(1,3);

        Console.WriteLine(first);
        Console.WriteLine(first.GetDecimal());

        Console.WriteLine(second);
        Console.WriteLine(second.GetDecimal());

        Console.WriteLine(third);
        Console.WriteLine(third.GetDecimal());

        Console.WriteLine(fourth);
        Console.WriteLine(fourth.GetDecimal());

        first.SetDenominator(3);

        Console.WriteLine(first.Equals(fourth));

        Fraction fifth = new Fraction(6,8);

        Console.WriteLine(fifth.Equals(third));

        Console.WriteLine(second.Equals(third));
    }
}