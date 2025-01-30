using _353502_STASEVICH_Lab1.Collections;

namespace _353502_STASEVICH_Lab1.Contracts;

public interface IService
{
    void AddTariff(string name, double price);

    void AddClientTariff(string name, string tariff);

    void AddClientTariffs(string name, MyCustomCollection<string> tariffs);

    double GetSumClient(string name);

    double GetTotalSum();

    int CountOrders(string name);
}