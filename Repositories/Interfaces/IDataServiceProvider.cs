namespace SRS.Repositories.Interfaces
{
    public interface IDataServiceProvider
    {
        T Get<T>();
    }
}
