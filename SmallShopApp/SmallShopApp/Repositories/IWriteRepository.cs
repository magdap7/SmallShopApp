using SmallShopApp.Entities;
namespace SmallShopApp.Repositories
{
    public interface IWriteRepository<in T> where T : class, IEntity
    {
        void LoadAll();
        void Add(T item);
        void Remove(T item);
        void Save();
    }
}
