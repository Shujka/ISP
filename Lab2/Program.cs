using _353502_STASEVICH_Lab1.Collections;
using _353502_STASEVICH_Lab1.Entities;

var client1 = new Client("client 1");
var client2 = new Client("client 2");
var clients = new MyCustomCollection<string>();
clients.Add(client1.Name);
clients.Add(client2.Name);
var tariffs = new MyCustomCollection<Tariff>();
var tariffsNames = new MyCustomCollection<string>();
tariffs.Add(new Tariff("tariff 1", 1));
tariffs.Add(new Tariff("tariff 2", 10));
tariffs.Add(new Tariff("tariff 3", 100));
tariffs.Add(new Tariff("tariff 4", 1000));
tariffsNames.Add("tariff 1");
tariffsNames.Add("tariff 2");
tariffsNames.Add("tariff 3");
tariffsNames.Add("tariff 4");

var myService = new Service(clients, tariffs);
Journal journal =  new Journal();
myService.TariffAdded += journal.TariffLog;
myService.ClientAdded += journal.ClientLog;
myService.Purchased += (Client client, Tariff tariff)=>
    Console.WriteLine($"клиент {client.Name} приобрел тариф {tariff.Name}");

myService.AddTariff("tariff 5", 10000);
myService.AddTariff("tariff 6", 1000);
myService.AddClientTariff("client 2", "tariff 2");
myService.AddClientTariffs("client 3", tariffsNames);
myService.AddClientTariff("client 6", "tariff 6");
tariffsNames.Remove("tariff 1");
tariffsNames.Remove("tariff 2");
tariffsNames.Add("tariff 5");
journal.ShowAllEvents();
myService.AddClientTariffs("client 3", tariffsNames);

// демонстрация работы с исключениями
clients.Reset();
for (int i = 0; i < 3; i++)
{
    try
    {
        clients.Next();
    }
    catch (Exception e)
    {
        Console.WriteLine("exception. Index is out of range");
        Console.WriteLine(e);
    }
}

try
{
    clients.Remove("client 10");
}
catch (Exception e)
{
    Console.WriteLine("exception. Element not found");
    Console.WriteLine($"Message {e.Message}");
    Console.WriteLine($"StackTrace {e.StackTrace}");
}
finally
{
    Console.WriteLine("Here's the final result");
}

clients.Remove("client 1");
clients.Remove("client 2");
clients.Reset();
try
{
    clients.Next();
}
catch (MyCustomException e)
{
    Console.WriteLine($"My exception!!! {e.Message}");
}