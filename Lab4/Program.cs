using _353502_STASEVICH_Lab4.Entities;

var directoryInfo = new DirectoryInfo("../../../STASEVICH_Lab4");
Console.WriteLine(directoryInfo.Name);
var fullPath = directoryInfo.FullName;
Console.WriteLine(fullPath);
if (Directory.Exists(directoryInfo.FullName))
{
    Directory.Delete(directoryInfo.FullName, true);
    Console.WriteLine($"Deleted directory {directoryInfo.Name}");
}

Directory.CreateDirectory(directoryInfo.FullName);

List<string> extentions = new List<string> { ".txt", ".rtf", ".dat", ".inf" };

for (int i = 0; i < 10; i++)
{
    int type = new Random().Next(0, 4);
    string fileName = Path.GetRandomFileName();
    fileName = Path.GetFileNameWithoutExtension(fileName) + extentions[type];
    File.Create(fullPath + "/" + fileName).Close();
    Console.WriteLine($"generated {fileName}");
}

var files = Directory.GetFiles(fullPath);

foreach (var file in files)
{
    Console.WriteLine($"File {Path.GetFileNameWithoutExtension(file)} has extension {Path.GetExtension(file)}");
}

List<Passenger> passengers = new List<Passenger>();
passengers.Add(new Passenger("Masha", 5, true));
passengers.Add(new Passenger("Petya", 4, false));
passengers.Add(new Passenger("Vanya", 3, false));
passengers.Add(new Passenger("Anya", 2, true));
passengers.Add(new Passenger("Moksim", 1, false));

FileService myFileService = new FileService();

myFileService.SaveData(passengers, Path.Combine(fullPath, "passengers.txt"));
File.Move(Path.Combine(fullPath , "passengers.txt"), Path.Combine(fullPath , "newpassengers.txt"));
File.Delete(Path.Combine(fullPath ,"passengers.txt"));

var newPassengers = myFileService.ReadFile(Path.Combine(fullPath, "newpassengers.txt")).ToList();

foreach (var passenger in
         newPassengers)
{
    string gender = passenger.Gender ? "Woman" : "Man";
    Console.WriteLine($"Name = {passenger.Name}, Id = {passenger.Id}, Gender = {gender}");
}

var sortedPassengers = newPassengers.OrderBy(x => x, new MyCustomComparer()).ToList();

Console.WriteLine("Sorted Passengers by Name: ");

foreach (var passenger in sortedPassengers)
{
    string gender = passenger.Gender ? "Woman" : "Man";
    Console.WriteLine($"Name = {passenger.Name}, Id = {passenger.Id}, Gender = {gender}");
}

var sortedPassengers2 = newPassengers.OrderBy(x => x.Id).ToList();

Console.WriteLine("Sorted Passengers by Id: ");

foreach (var passenger in sortedPassengers2)
{
    string gender = passenger.Gender ? "Woman" : "Man";
    Console.WriteLine($"Name = {passenger.Name}, Id = {passenger.Id}, Gender = {gender}");
}