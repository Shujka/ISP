using System.Numerics;
using _353502_STASEVICH_Lab1.Collections;

namespace _353502_STASEVICH_Lab1.Entities;

public class Client
{
    public MyCustomCollection<Tariff> TariffList { get; set; } = new MyCustomCollection<Tariff>();
    public string Name { get; }
    public Client(string name)
    {
        Name = name;
    }
}