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

myService.AddTariff("tariff 5", 10000);
myService.AddClientTariff("client 2", "tariff 2");
myService.AddClientTariffs("client 3", tariffsNames);
tariffsNames.Remove("tariff 1");
tariffsNames.Remove("tariff 2");
tariffsNames.Add("tariff 5");
for (int i = 0; i < tariffsNames.Count; i++)
{
    Console.WriteLine(tariffsNames[i]);
}
myService.AddClientTariffs("client 3", tariffsNames);

for (int i = 0; i < 5; i++)
{
    string client = "client " + i;
    Console.WriteLine($"client {i} sum tariffs {myService.GetSumClient(client)}");
}

Console.WriteLine($"Сумма всех заказанных услуг {myService.GetTotalSum()}");

for (int i = 0; i < 6; i++)
{
    string tariff = "tariff " + i;
    Console.WriteLine($"tariff {i} has {myService.CountOrders(tariff)} orders");
}

