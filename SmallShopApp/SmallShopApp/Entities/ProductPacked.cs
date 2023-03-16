
namespace SmallShopApp.Entities
{
    public class ProductPacked : Product
    {
        public int? Quantity { get; set; }
        public override string ToString()
        {
            return base.ToString() + $", Quantity: {Quantity}";
        }
    }
}
