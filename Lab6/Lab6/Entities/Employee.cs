namespace _353502_STASEVICH_Lab6.Entities;

[Serializable]
public class Employee
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public bool Gender { get; set; }

    public Employee()
    {
        
    }

    public Employee(string? name, int age, bool gender)
    {
        Name = name;
        Age = age;
        Gender = gender;
    }
}