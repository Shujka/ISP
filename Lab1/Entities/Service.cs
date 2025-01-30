using System.Numerics;
using _353502_STASEVICH_Lab1.Collections;
using _353502_STASEVICH_Lab1.Contracts;

namespace _353502_STASEVICH_Lab1.Entities;

public class Service : IService
{
    private MyCustomCollection<Tariff> _listTariffs = new MyCustomCollection<Tariff>();
    private MyCustomCollection<Client> _listClients = new MyCustomCollection<Client>();

    public Service() { }
    public Service(MyCustomCollection<string> clients, MyCustomCollection<Tariff> tariffs)
    {
        _listTariffs = tariffs;
        for (int i = 0; i < clients.Count; i++)
        {
            AddClient(clients[i]);
        }
    }
    
    public void AddTariff(string name, double price)
    {
        _listTariffs.Add(new Tariff(name, price));
    }

    // set cursor to the Client(name) if name is in list. Return false if client not in list
    private bool TryFindClient(string name)
    {
        bool flag = false;
        _listClients.Reset();
        for (int i = 0; i < _listClients.Count - 1; i++)
        {
            if (_listClients.Current().Name == name)
            {
                flag = true;
                break;
            }
            _listClients.Next();
        }

        if (_listClients.Count != 0 && _listClients.Current().Name == name)
        {
            flag = true;
        }

        return flag;

    }

    // add tariff to the current client. Tariff must be in list
    private void AddClientTariff(string tariff)
    {
        bool flag = false;
        // find tariff
        _listTariffs.Reset();
        for (int i = 0; i < _listTariffs.Count - 1; i++)
        {
            if (_listTariffs.Current().Name == tariff)
            {
                flag = true;
                break;
            }
            _listTariffs.Next();
        }

        if (_listTariffs.Count != 0 && _listTariffs.Current().Name == tariff)
        {
            flag = true;
        }
        
        if (!flag)
        {
            // tariff not in list
            throw new Exception("tariff not found");
        }

        _listClients.Current().TariffList.Add(_listTariffs.Current());
    }
    
    public void AddClient(string name)
    {
        bool flag = TryFindClient(name);
        if (!flag)
        {
            // client not in list
            _listClients.Add(new Client(name));
            if(_listClients.Count != 1)
                _listClients.Next();
        }
        else
        {
            throw new Exception("person already in list");
        }
    }
    
    // add tariff to the Client named name
    public void AddClientTariff(string name, string tariff)
    {
        bool flag = TryFindClient(name);
        if (!flag)
        {
            // client not in list
            _listClients.Add(new Client(name));
            if(_listClients.Count != 1)
                _listClients.Next();
        }
        AddClientTariff(tariff);
    }

    // add list of tariffs to the Client named name
    public void AddClientTariffs(string name, MyCustomCollection<string> tariffs  )
    {
        bool flag = TryFindClient(name);
        if (!flag)
        {
            // client not in list
            _listClients.Add(new Client(name));
            if(_listClients.Count != 1)
                _listClients.Next();
        }
        tariffs.Reset();
        if (tariffs.Count != 0)
        {
            for (int i = 0; i < tariffs.Count - 1; i++)
            {
                AddClientTariff(tariffs.Current());
                tariffs.Next();
            }
            AddClientTariff(name, tariffs.Current());
        }
    }

    public double GetSumClient(string name)
    {
        bool flag = TryFindClient(name);
        if (!flag)
            return 0;
        var currentTariffs = _listClients.Current().TariffList;
        currentTariffs.Reset();
        double sum = 0;
        for (int i = 0; i < currentTariffs.Count - 1; i++)
        {
            sum += currentTariffs.Current().Price;
            currentTariffs.Next();
        }
        if(currentTariffs.Count != 0)
            sum += currentTariffs.Current().Price;
        return sum;
    }
    public double GetTotalSum()
    {
        double returnSum = 0;
        for (int i = 0; i < _listClients.Count; i++)
        {
            var listTariff = _listClients[i].TariffList;
            var addSum = MyCustomCollection<Tariff>.Sum(listTariff);
            if(addSum != null)
                returnSum += addSum!.Price;
        }
        return returnSum;
    }
    public int CountOrders(string name)
    {
        int sum = 0;
        for (int i = 0; i < _listClients.Count; i++)
        {
            var listClientsTariff = _listClients[i].TariffList;
            listClientsTariff.Reset();
            for(int j = 0; j < listClientsTariff.Count; j ++)
            {
                if (listClientsTariff[j].Name == name)
                {
                    sum++;
                }
            }
        }
        return sum;
    }
}