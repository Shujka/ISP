using System.Numerics;

namespace _353502_STASEVICH_Lab1.Entities;

public class Tariff : IAdditionOperators<Tariff, Tariff, Tariff>
{
    public string Name { get; }
    public double Price { get; }
    
    public Tariff()
    {
        Name = "empty name";
        Price = 0;
    }
    public Tariff(double price)
    {
        Name = "empty name";
        Price = price;
    }
    public Tariff(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public static Tariff operator +(Tariff left, Tariff right)
    {
        return new Tariff(left.Price + right.Price);
    }
}