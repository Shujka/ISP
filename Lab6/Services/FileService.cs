using _353502_STASEVICH_Lab6.Interfaces;
using System.Text.Json;
namespace Services;

public class FileService<T> : IFileService<T> where T : class
{
    public IEnumerable<T> ReadFile(string fileName)
    {
        try
        {
            using (var stream = File.OpenRead(fileName))
            {
                var collection = JsonSerializer.Deserialize<IEnumerable<T>>(stream) as IEnumerable<T>;
                return collection;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    public void SaveData(IEnumerable<T> data, string fileName)
    {
        try
        {
            using (var stream = File.CreateText(fileName))
            {
                var serialized = JsonSerializer.Serialize(data);
                stream.Write(serialized);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while serializing data");
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    
    
}