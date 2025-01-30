using _353502_STASEVICH_Lab4.Interfaces;
namespace _353502_STASEVICH_Lab4.Entities;

public class FileService: IFIleService<Passenger>
{
    public IEnumerable<Passenger> ReadFile(string fileName)
    {
        using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
        {
            string name = "";
            int id = 0;
            bool gender = false;
            while (reader.PeekChar() > -1)
            {
                try
                {
                    name = reader.ReadString();
                    id = reader.ReadInt32();
                    gender = reader.ReadBoolean();
                }
                catch
                {
                    Console.WriteLine("Error reading file");
                    throw;
                }
                yield return new Passenger(name, id, gender);
            }
        }
    }

    public void SaveData(IEnumerable<Passenger> data, string fileName)
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
            Console.WriteLine($"Файл {fileName} удален");
        }
        File.Create(fileName).Close();
        using (BinaryWriter writer = new BinaryWriter(File.Open(fileName, FileMode.Open)))
        {
            foreach (var passenger in data)
            {
                try
                {
                    writer.Write(passenger.Name);
                    writer.Write(passenger.Id);
                    writer.Write(passenger.Gender);
                }
                catch
                {
                    Console.WriteLine("Запись не удалась");
                }
            }
        }
    }
}