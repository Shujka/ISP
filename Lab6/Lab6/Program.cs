using System.Collections;
using _353502_STASEVICH_Lab6.Entities;
using System.Reflection;

var firm = new List<Employee>();
firm.Add(new Employee("Maria", 20, false));
firm.Add(new Employee("John", 18, true));
firm.Add(new Employee("Jane", 29, false));
firm.Add(new Employee("Jack", 41, true));
firm.Add(new Employee("Jenny", 19, false));
firm.Add(new Employee("Jill", 37, false));

var fileName = "D:\\Work\\БГУИР\\ИСП\\353502_STASEVICH_Lab6\\353502_STASEVICH_Lab6\\DataJson.json";

var asm = Assembly.LoadFrom("D:\\Work\\БГУИР\\ИСП\\353502_STASEVICH_Lab6\\Services\\bin\\Debug\\net8.0\\Services.dll");
Type genericType = asm.GetType("Services.FileService`1");
Type specificType = genericType.MakeGenericType(typeof(Employee));
object fileServiceInstance = Activator.CreateInstance(specificType);
MethodInfo saveData = specificType.GetMethod("SaveData");
saveData.Invoke(fileServiceInstance, [firm, fileName]);
MethodInfo loadData = specificType.GetMethod("ReadFile");
object result = loadData.Invoke(fileServiceInstance, [fileName]);
IEnumerable enumerableResult = (IEnumerable)result;
List<Employee> res = enumerableResult.Cast<Employee>().ToList();
foreach (var item in res)
{
    Console.WriteLine($"name = {item.Name}, age = {item.Age}, gender = {item.Gender}");
} 