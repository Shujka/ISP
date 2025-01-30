using _353502_STASEVICH_Lab1.Collections;

namespace _353502_STASEVICH_Lab1.Entities;

public class Journal
{
    public MyCustomCollection<Tariff> _tariffList { get;} = new MyCustomCollection<Tariff>();
    public MyCustomCollection<Client> _clientList { get;} = new MyCustomCollection<Client>();


    public void TariffLog(Tariff tariff)
    {
        Console.WriteLine(tariff.Name);
        _tariffList.Add(tariff);
    }
    
    public void ClientLog(Client client)
    {
        Console.WriteLine(client.Name);
        _clientList.Add(client);
    }
    
    public void ShowClientEvents()
    {
        foreach (var client in _clientList)
        {
            Console.WriteLine($"Запись в журнале: был добавлен новый клиент {client.Name}");
        }
    }
    
    public void ShowTariffEvents()
    {
        foreach (var tariff in _tariffList)
        {
            Console.WriteLine($"Запись в журнале: был добавлен новый тариф {tariff.Name}");
        }
    }

    public void ShowAllEvents()
    {
        ShowTariffEvents();
        ShowClientEvents();
    }
}