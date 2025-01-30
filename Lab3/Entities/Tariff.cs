using System.Numerics;

namespace _353503_STASEVICH_Lab3.Entities;

public class Tariff
{
    public string Name { get; }
    public double Price { get; }

    public Tariff()
    {
        Name = "no name";
        Price = 0;
    }

    public Tariff(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public static Tariff operator +(Tariff left, Tariff right)
    {
        return new Tariff(left.Name, left.Price + right.Price); 
    }
}