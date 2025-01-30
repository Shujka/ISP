using _353502_STASEVICH_Lab1.Interfaces;

using System.Numerics;

namespace _353502_STASEVICH_Lab1.Collections;
public class MyCustomCollection<T> : ICustomCollection<T>
{
    class Node<T>
    {
        public T Value { get; set; }
        public Node<T>? L { get; set; } = null;
        public Node<T>? R { get; set; } = null;
    }

    private Node<T>? _head = null;
    private Node<T>? _curPosition = null;
    private Node<T>? _tail = null;
    private int _size = 0;

    public T this[int index]
    {
        get
        {
            if (_size <= index)
            {
                throw new IndexOutOfRangeException();
            }

            var _temp = _head;
            for (int i = 0; i < index; i++)
            {
                _temp = _temp.R;
            }

            return _temp.Value;
        }
        set
        {
            if (_size <= index)
            {
                throw new IndexOutOfRangeException();
            }

            var _temp = _head;
            for (int i = 0; i < index; i++)
            {
                _temp = _temp.R;
            }

            _temp.Value = value;
        }

    }

    public void Reset()
    {
        _curPosition = _head;
    }

    public void Next()
    {
        if (_size != 0)
        {
            if(_curPosition.R != null)
                _curPosition = _curPosition.R;
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
        else
        {
            throw new Exception("empty list");
        }
    }

    public T Current()
    {
        if (_curPosition != null)
        {
            return _curPosition.Value;
        }
        throw new Exception("empty collection");
    }

    public int Count
    {
        get => _size;
    }
    
    public void Add(T item)
    {
        Node<T> _temp = new Node<T>();
        _temp.Value = item;

        _size++;
        
        if (_size == 1)
        {
            _curPosition = _temp;
            _head = _temp;
            _tail = _temp;
        }
        else
        {
            _temp.L = _tail;
            _tail.R = _temp;
            _tail = _tail.R;
        }
    }

    public void Remove(T item)
    {
        Node<T>? _temp = _head;
        while (_temp != null && !Equals(_temp.Value, item))
        {
            _temp = _temp.R;
        }

        if (_temp != null && Equals(_temp.Value, item))
        {
            if(_size == 1)
            {
                _size = 0;
                _curPosition = null;
                _head = null;
                _tail = null;
                return;
            }

            _size--;
            
            if (_curPosition == _temp)
            {
                // move cursor
                if (_temp == _head)
                {
                    _head = _head.R;
                    _head.L = null;
                    _curPosition = _head;
                    return;
                }

                if (_temp == _tail)
                {
                    _tail = _tail.L;
                    _tail.R = null;
                    _curPosition = _tail;
                    return;
                }

                _curPosition = _temp.L;
                _temp.L.R = _temp.R;
                _temp.R.L = _temp.L;
                return;
            }

            if (_temp == _head)
            {
                _head = _head.R;
                _head.L = null;
                return;
            }

            if (_temp == _tail)
            {
                _tail = _tail.L;
                _tail.R = null;
                return;
            }

            _temp.L.R = _temp.R;
            _temp.R.L = _temp.L;
        }
        
        // no element found
    }

    public T RemoveCurrent()
    {
        if (_size == 0)
        {
            throw new Exception("empty list");
        }

        T returnValue = _curPosition.Value;
        
        _size--;
        if (_curPosition == _head)
        {
            _head = _head.R;
            _head.L = null;
            _curPosition = _head;
            return returnValue;
        }

        if (_curPosition == _tail)
        {
            _tail = _tail.L;
            _tail.R = null;
            _curPosition = _tail;
            return returnValue;
        }

        _curPosition = _curPosition.L;
        _curPosition.L.R = _curPosition.R;
        _curPosition.R.L = _curPosition.L;
        return returnValue;
    }

    public static T? Sum<T>(MyCustomCollection<T> collection) where T : IAdditionOperators<T, T, T>
    {
        if(collection.Count == 0)
            return default(T);
        T sum = collection[0];
        collection.Reset();
        for (int i = 1; i < collection.Count; i++)
        {
            collection.Next();
            sum += collection.Current();
        }

        return sum;
    }
}