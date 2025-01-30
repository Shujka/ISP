using Work.Domain.Models;
using SerializerLib;
using Microsoft.Extensions.Configuration.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

string fileName = "D:\\Work\\БГУИР\\ИСП\\STASEVICH_353502_Lab5\\STASEVICH_353502_Lab5\\appsettings.json";

string json = File.ReadAllText(fileName);
JObject jsonObj = JObject.Parse(json);

jsonObj["XmlFileName"] = "D:\\Work\\БГУИР\\ИСП\\STASEVICH_353502_Lab5\\STASEVICH_353502_Lab5\\DataXml.xml";
jsonObj["JsonFileName"] = "D:\\Work\\БГУИР\\ИСП\\STASEVICH_353502_Lab5\\STASEVICH_353502_Lab5\\DataJson.json";
jsonObj["LinqFileName"] = "D:\\Work\\БГУИР\\ИСП\\STASEVICH_353502_Lab5\\STASEVICH_353502_Lab5\\DataLinq.xml";

string updatedJson = jsonObj.ToString();

File.WriteAllText(fileName, updatedJson);

IConfiguration config = new ConfigurationBuilder() .AddJsonFile(fileName, optional: false, reloadOnChange: true) .Build();

Hospital hospital1 = new Hospital("Hospital №10", new Reception("Ann", 3, "+375291112233"), "Minsk");
Hospital hospital2 = new Hospital("Hospital №2", new Reception("Alex", 2, "+375291234565"), "Brest");
Hospital hospital3 = new Hospital("Hospital №1", new Reception("Anton", 1, "+375331234567"), "Homel");
Hospital hospital4 = new Hospital("Hospital №8", new Reception("Mary", 3, "+37529778856"), "Minsk");
Hospital hospital5 = new Hospital("Hospital №3", new Reception("Victor", 2, "+375337070353"), "Minsk");
Hospital hospital6 = new Hospital("Hospital №1", new Reception("Max", 1, "+375331234567"), "Brest");

var hospitalLst = new List<Hospital>();
hospitalLst.Add(hospital1);
hospitalLst.Add(hospital2);
hospitalLst.Add(hospital3);
hospitalLst.Add(hospital4);
hospitalLst.Add(hospital5);
hospitalLst.Add(hospital6);

var serializer = new Serializer();
serializer.SerializeByJSON(hospitalLst, config["JsonFileName"]);
var deserialised = serializer.DeSerializeByJSON(config["JsonFileName"]).ToList();

Console.Write("serialization and deserialisation with JSON is ");
for (int i = 0; i < hospitalLst.Count; i++)
{
    var v1 = hospitalLst[i];
    var v2 = deserialised[i];
    if (!v1.Equals(v2))
    {
        Console.Write("un");
        break;
    }
}
Console.WriteLine("successful");

serializer.SerializeByXML(hospitalLst, config["XmlFileName"]);
deserialised = serializer.DeSerializeByXML(config["XmlFileName"]).ToList();

Console.Write("serialization and deserialisation with XML is ");
for (int i = 0; i < hospitalLst.Count; i++)
{
    var v1 = hospitalLst[i];
    var v2 = deserialised[i];
    if (!v1.Equals(v2))
    {
        Console.Write("un");
        break;
    }
}
Console.WriteLine("successful");

serializer.SerializeByLINQ(hospitalLst, config["LinqFileName"]);
deserialised = serializer.DeSerializeByLINQ(config["LinqFileName"]).ToList();

Console.Write("serialization and deserialisation with XML is ");
for (int i = 0; i < hospitalLst.Count; i++)
{
    var v1 = hospitalLst[i];
    var v2 = deserialised[i];
    if (!v1.Equals(v2))
    {
        Console.Write("un");
        break;
    }
}
Console.WriteLine("successful");