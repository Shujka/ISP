namespace _353503_STASEVICH_Lab3.Entities;

public class Client
{
    public string Name { get; }
    public List<Tariff> _lstTariffs { get; set; } = new List<Tariff>();

    public Client(string name)
    {
        Name = name;
    }
}