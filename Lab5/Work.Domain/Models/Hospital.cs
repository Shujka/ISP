using System.Text.Json.Serialization;

namespace Work.Domain.Models;

[Serializable]
public class Hospital : IEquatable<Hospital>
{
    public string Name { get; set; }
    public Reception Reception { get; set; }
    public string Address { get; set; }
    public bool Equals(Hospital? other)
    {
       return ((Name == other?.Name) && Reception.Equals(other.Reception) && (Address == other.Address));
    }

    [NonSerialized]
    private int _t = 0;
    
    public Hospital()
    {
        
    }
    public Hospital(string name, Reception reception, string address)
    {
        Name = name;
        Reception = reception;
        Address = address;
    }
}