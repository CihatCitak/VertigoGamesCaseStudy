namespace SaveLoad.Save.Interfaces
{
    public interface ISaveService<T>
    {
        void Save(T data);
    }
}