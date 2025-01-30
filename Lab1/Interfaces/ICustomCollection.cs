namespace _353502_STASEVICH_Lab1.Interfaces;

public interface ICustomCollection<T>
{
    T this[int index] { get; set; }
    void Reset();
    void Next();
    T Current();
    T RemoveCurrent();
    int Count { get; }
    void Add(T item);
    void Remove(T item);
    
}