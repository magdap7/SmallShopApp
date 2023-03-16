
namespace SmallShopApp.Entities
{
    public class ProductWeighted : Product
    {
        public float? Weight { get; set; }
        public override string ToString()
        {
            return base.ToString() + $", Weight: {Weight}";
        }
    }
}
