using Newtonsoft.Json;

namespace SaveSystem.Interfaces
{
    public interface IConverter<T>
        where T : IData
    {
        JsonConverter Converter
        {
            get;
        }
    }
}