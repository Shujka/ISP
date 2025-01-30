using _353503_STASEVICH_Lab3.Entities;

var client1 = new Client("client 1");
var client2 = new Client("client 2");
var clients = new List<Client>();
clients.Add(new Client(client1.Name));
clients.Add(new Client(client2.Name));
var tariffs = new List<Tariff>();
var tariffsNames = new List<string>();
tariffs.Add(new Tariff("tariff 1", 10000));
tariffs.Add(new Tariff("tariff 2", 1000));
tariffs.Add(new Tariff("tariff 3", 100));
tariffs.Add(new Tariff("tariff 4", 1));
tariffsNames.Add("tariff 1");
tariffsNames.Add("tariff 2");
tariffsNames.Add("tariff 3");
tariffsNames.Add("tariff 4");

var myService = new Service(clients, tariffs);

myService.AddTariff("tariff 5", 10000);
myService.AddTariff("tariff 6", 1000);
myService.DoPurchase("client 1", "tariff 1");
myService.DoPurchase("client 1", "tariff 6");
myService.DoPurchase("client 1", "tariff 4");
myService.DoPurchase("client 4", "tariff 4");
myService.DoPurchase("client 2", "tariff 2");
myService.DoPurchase("client 3", "tariff 3");
myService.DoPurchase("client 3", "tariff 4");
myService.DoPurchase("client 3", "tariff 1");
myService.DoPurchase("client 6", "tariff 6");
tariffsNames.Remove("tariff 1");
tariffsNames.Remove("tariff 2");
tariffsNames.Add("tariff 5");
foreach (var tariff in tariffsNames)
{
    myService.DoPurchase("client 6", tariff);
}

for (int i = 1; i <= 6; i++)
{
    Console.WriteLine($"Client {i} has purchased {myService.GetClientSum("client " + i)}");
}
Console.WriteLine($"Client with max sum is {myService.GetMaxPaidName()}");
myService.DoPurchase("client 3", "tariff 1");
Console.WriteLine($"Client with max sum is {myService.GetMaxPaidName()}");

var listOfTariffs = myService.GetTariffNames();
Console.WriteLine("All tariff names:");
foreach (var tariff in listOfTariffs)
{
    Console.WriteLine(tariff);
}

Console.WriteLine($"Total sum is {myService.GetTotalSum()}");

for (int i = 1; i <= 6; i++)
{
    Console.WriteLine($"client {i} paid {myService.GetClientSum("client " + i)}");
}
Console.WriteLine($"{myService.CountClientsPaidMoreThanSum(0)} clients paid more than 0");
Console.WriteLine($"{myService.CountClientsPaidMoreThanSum(1)} clients paid more than 1");
Console.WriteLine($"{myService.CountClientsPaidMoreThanSum(999)} clients paid more than 999");
Console.WriteLine($"{myService.CountClientsPaidMoreThanSum(1000)} clients paid more than 1000");
Console.WriteLine($"{myService.CountClientsPaidMoreThanSum(10000)} clients paid more than 10000");
Console.WriteLine($"{myService.CountClientsPaidMoreThanSum(100000)} clients paid more than 100000");

myService.AddClient("client 5");
myService.DoPurchase("client 5", "tariff 1");
myService.DoPurchase("client 5", "tariff 1");
myService.DoPurchase("client 5", "tariff 1");

for (int i = 1; i <= 6; i++)
{
    var values = myService.GetClientTariffPriceSum("client " + i);
    Console.WriteLine($"client {i}");
    foreach (var item in values)
    {
        Console.WriteLine($"{item.Key} total sum {item.Value}");
    }
}