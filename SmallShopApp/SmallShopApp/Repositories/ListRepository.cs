using SmallShopApp.Entities;
using System.Text.Json;

namespace SmallShopApp.Repositories
{
    public class ListRepository<T> : IRepository<T> where T : class, IEntity, new()
    {
        protected List<T> _items = new();
        public event EventHandler<T>? ItemAdded, ItemRemoved;
        //string dirPath = Path.GetFullPath("SmallShopApp\\..\\..\\..\\..\\Data\\");


        public void LoadAll()
        {
            _items = ReadObjectsFromFile<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public T GetById(int id)
        {
            if (id < 1)
                throw new Exception();
            else
                return _items.Single(item => item.Id == id);
        }

        public void Add(T item)
        {
            if (_items.Count == 0)
            {
                _items.Add(item);
                item.Id = 1;
            }
            else
            {
                var maxID = _items.Max(p => p.Id);
                item.Id = maxID + 1;
                _items.Add(item);
            }
            ItemAdded?.Invoke(this, item);
        }

        public void Remove(T item)
        {
            if (item == null)
                throw new Exception();
            else
            {
                _items.Remove(item);
                ItemRemoved?.Invoke(this, item);
            }
        }

        public void Save()
        {
            WriteObjectsToFile<T>(_items);
        }

        List<T> ReadObjectsFromFile<T>() where T : class, IEntity, new()
        {
            T obj = new();
            var fileName = "_" + obj.GetType().Name + ".txt";
            List<T> objList = new List<T>();

            if (File.Exists(fileName))
            {
                using (var reader = File.OpenText(fileName))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        obj = JsonSerializer.Deserialize<T>(line);
                        if (obj != null)
                            objList.Add(obj);
                        line = reader.ReadLine();
                    }
                }
            }
            return objList.ToList();
        }

        void WriteObjectsToFile<T>(List<T> items) where T : class, IEntity, new()
        {
            T obj = new();
            var fileName = "_" + obj.GetType().Name + ".txt";

            using (var writer = File.CreateText(fileName))
            {
                int itemCount = this._items.Count;
                for (int i = 0; i < itemCount; i++)
                {
                    T item = items[i];
                    string jsonObj = JsonSerializer.Serialize<T>(item);
                    writer.WriteLine(jsonObj);
                }
            }
        }
    }
}
