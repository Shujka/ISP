namespace _353502_STASEVICH_Lab4.Entities;

public class MyCustomComparer : IComparer<Passenger>
{
    
    public int Compare(Passenger? x, Passenger? y)
    {
        return String.CompareOrdinal(x.Name, y.Name);
    }
}