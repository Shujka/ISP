namespace _353503_STASEVICH_Lab3.Entities;

public class Service
{
    Dictionary<string, Tariff> _lstTariffs = new Dictionary<string, Tariff>();
    List<Client> _lstClients = new List<Client>();

    public delegate void Add<T>(T value);

    public delegate void Purchase(Client client, Tariff tariff);

    public event Add<Client> AddClientHandler;
    public event Add<Tariff> AddTariffHandler;
    public event Purchase AddPurchaseHandler;

    private void SetDefaultevents()
    {
        AddClientHandler += value => Console.WriteLine($"New client {value.Name} registered");
        AddTariffHandler += value => Console.WriteLine($"Tariff {value.Name} registered. Price {value.Price} was set.");
        AddPurchaseHandler += (client, tariff) => Console.WriteLine($"{client.Name} purchased {tariff.Name}");
    }

    public Service()
    {
        SetDefaultevents();
    }

    public Service(List<Client> clients, List<Tariff> tariffs)
    {
        SetDefaultevents();
        _lstClients = clients;
        foreach (var tariff in tariffs)
        {
            AddTariff(tariff.Name, tariff.Price);
        }
    }

    void OnTariffAdded(Tariff tariff)
    {
        AddTariffHandler?.Invoke(tariff);
    }

    void OnClientAdded(Client client)
    {
        AddClientHandler?.Invoke(client);
    }

    void OnAddPurchaseHandler(Client client, Tariff tariff)
    {
        AddPurchaseHandler?.Invoke(client, tariff);
    }

    public void AddTariff(string name, double price)
    {
        if (_lstTariffs.ContainsKey(name))
        {
            throw new Exception($"Tariff {name} already exists");
        }

        _lstTariffs.Add(name, new Tariff(name, price));
        OnTariffAdded(new Tariff(name, price));
    }

    public void AddClient(string name)
    {
        if (_lstClients.Contains(new Client(name)))
        {
            throw new Exception($"Client {name} already exists");
        }

        _lstClients.Add(new Client(name));
        OnClientAdded(new Client(name));
    }

    public void DoPurchase(string name, string tariff)
    {
        if (_lstTariffs.ContainsKey(tariff))
        {
            if (_lstClients.All(x => x.Name != name))
            {
                foreach (var client in _lstClients)
                {
                    Console.WriteLine(client.Name);
                }

                AddClient(name);
            }

            foreach (var client in _lstClients)
            {
                if (client.Name == name)
                {
                    client._lstTariffs.Add(_lstTariffs[tariff]);
                    OnAddPurchaseHandler(client, _lstTariffs[tariff]);
                    break;
                }
            }
        }
        else
        {
            throw new Exception($"Tariff {tariff} does not exist");
        }

    }

    public double GetClientSum(string name)
    {
        if (_lstClients.Any(x => x.Name == name))
        {
            var tariffs = from i in _lstClients
                where i.Name == name
                select i._lstTariffs;
            var sum = from i in tariffs
                select i.Sum(t => t.Price);
            return sum.First();
        }
        return 0;
    }

    public int CountPurchases(string tariff)
    {
        int sum = 0;
        foreach (var client in _lstClients)
        {
            foreach (var tariffs in client._lstTariffs)
            {
                if (tariffs.Name == tariff)
                {
                    sum++;
                }
            }
        }

        return sum;
    }

    public List<string> GetTariffNames()
    {
        var tariffs = from i in _lstTariffs
            orderby i.Value.Price
            select i.Key;

        return tariffs.ToList();
    }

    public double GetTotalSum()
    {
        var allTariffs = from i in _lstClients
            select i._lstTariffs;
        var totalSum = from i in allTariffs
            select i.Sum(t => t.Price);
        return totalSum.Sum();
    }

    public string GetMaxPaidName()
    {

        if (_lstClients.Count == 0)
        {
            throw new Exception("empty list");
        }

        var allTariffs = from i in _lstClients
            select i._lstTariffs;
        var totalSum = from i in allTariffs
            select i.Sum(t => t.Price);
        var maxValue = totalSum.Max();
        var maxPaidName = from i in _lstClients
            where Math.Abs(i._lstTariffs.Sum(t => t.Price) - maxValue) < 0.0000000001
            select i.Name;
        return maxPaidName.First();
    }

    public int CountClientsPaidMoreThanSum(double sum)
    {
        var cnt = _lstClients.Aggregate(0, (current_cnt, client) => (GetClientSum(client.Name) > sum ? current_cnt + 1 : current_cnt));
        return cnt;
    }
    
    public Dictionary<string, double> GetClientTariffPriceSum(string name)
    {
        var returnValues = _lstClients
            .FirstOrDefault(x => x.Name == name)
            ?._lstTariffs
                    .GroupBy(x => x.Name)
                    .ToDictionary(g => 
                                            g.Key, g => g.Sum(x => x.Price));
        if(returnValues is null)
            throw new Exception($"Client {name} does not exist");
        return returnValues;
    }
}