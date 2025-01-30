using Work.Domain.Models;

namespace Work.Domain.Interfaces;

public interface ISerializer
{
    IEnumerable<Hospital> DeSerializeByLINQ(string fileName);
    IEnumerable<Hospital> DeSerializeByXML(string fileName);
    IEnumerable<Hospital> DeSerializeByJSON(string fileName);
    void SerializeByLINQ(IEnumerable<Hospital> hospital, string fileName);
    void SerializeByXML(IEnumerable<Hospital> hospital, string fileName);
    void SerializeByJSON(IEnumerable<Hospital> hospital, string fileName);

}