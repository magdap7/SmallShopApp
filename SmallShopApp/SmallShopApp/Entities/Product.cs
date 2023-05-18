
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace SmallShopApp.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public override string ToString() => $"Id: {Id}, Name: {Name}, Price: {Price}";
    }
}
