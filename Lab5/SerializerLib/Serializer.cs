using System.Text.Json;
using System.Xml.Linq;
using Work.Domain.Interfaces;
using Work.Domain.Models;
using System.Xml.Serialization;

namespace SerializerLib;

public class Serializer : ISerializer
{
    public IEnumerable<Hospital> DeSerializeByLINQ(string fileName)
    {
        XDocument xdoc = XDocument.Load(fileName);
        var root = xdoc.Element("hospitals");
        
        List<Hospital> hospitals = new List<Hospital>();
        
        foreach (var hospital in root.Elements("hospital"))
        {
            var xname = hospital.Attribute("Name").Value;
            var xaddress = hospital.Attribute("Address").Value;
            var xreception = hospital.Element("reception");
            var xnameemployee = xreception.Attribute("NameEmployee").Value;
            var xemloyeesnumber = xreception.Attribute("EmployeesNumber").Value;
            var xphone = xreception.Attribute("PhoneNumber").Value;
            hospitals.Add(new Hospital(xname, new Reception(xnameemployee, Convert.ToInt32(xemloyeesnumber), xphone), xaddress));
        }
        return hospitals;
    }

    public IEnumerable<Hospital> DeSerializeByXML(string fileName)
    {
        
        using (var stream = File.OpenRead(fileName))
        {
            var fromxml = new XmlSerializer(typeof(List<Hospital>)).Deserialize(stream) as List<Hospital>;
            return fromxml;
        }
        
    }

    public IEnumerable<Hospital> DeSerializeByJSON(string fileName)
    {
        var fromjson = (JsonSerializer.Deserialize<List<Hospital>>(File.ReadAllText(fileName)) ?? throw new InvalidOperationException());
        return fromjson;
    }

    public void SerializeByLINQ(IEnumerable<Hospital> hospital, string fileName)
    {
        XDocument xdoc = new XDocument();
        XElement root = new XElement("hospitals");
        foreach (var h in hospital)
        {
            root.Add(new XElement("hospital", 
                new XAttribute("Name", h.Name), 
                new XAttribute("Address", h.Address), 
                new XElement("reception", 
                    new XAttribute("NameEmployee", h.Reception.NameEmployee), 
                    new XAttribute("EmployeesNumber", h.Reception.EmployeesNumber),
                    new XAttribute("PhoneNumber", h.Reception.PhoneNumber))
                ));
        }
        xdoc.Add(root);
        xdoc.Save(fileName);
    }

    public void SerializeByXML(IEnumerable<Hospital> hospital, string fileName)
    {
        using (var stream = File.CreateText(fileName))
        {
            new XmlSerializer(typeof(List<Hospital>)).Serialize(stream, hospital);
        }
    }

    public void SerializeByJSON(IEnumerable<Hospital> hospital, string fileName)
    {
        using(var stream = File.CreateText(fileName))
        {
            var json = JsonSerializer.Serialize(hospital);
            stream.Write(json);
        }

    }
}