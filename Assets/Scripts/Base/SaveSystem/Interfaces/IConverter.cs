using Newtonsoft.Json;

namespace Base.SaveSystem.Interfaces
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