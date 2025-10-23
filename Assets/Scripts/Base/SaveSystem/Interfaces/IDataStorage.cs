namespace SaveSystem.Interfaces
{
    public interface IDataStorage
    {
        public void Save();

        public void Load();

        public void Clear();
    }
}