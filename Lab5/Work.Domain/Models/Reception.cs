namespace Work.Domain.Models;

[Serializable]  
public class Reception : IEquatable<Reception>
{
    public string NameEmployee { get; set; }
    public int EmployeesNumber { get; set; }
    public string PhoneNumber { get; set; }

    public bool Equals(Reception? other)
    {
        return ((NameEmployee == other.NameEmployee) && (EmployeesNumber == other.EmployeesNumber) && (PhoneNumber == other.PhoneNumber));
    }

    public Reception()
    {
        
    }
    public Reception(string nameEmployee, int employeesNumber, string phoneNumber)
    {
        NameEmployee = nameEmployee;
        EmployeesNumber = employeesNumber;
        PhoneNumber = phoneNumber;
    }
}