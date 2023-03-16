using SmallShopApp;
using SmallShopApp.Data;
using SmallShopApp.Entities;
using SmallShopApp.Repositories;

Console.WriteLine("Hello in my shop!");

//var pW = new ProductWeighted { Name = "jablka", Price = 1.3f, Weight = 2.5f };
//var pP = new ProductPacked { Name = "sok", Price = 6, Quantity = 1 };
//Console.WriteLine(pW.ToString());
//Console.WriteLine(pP.ToString());


var productWeightedRepository = new SqlRepository<ProductWeighted>(new SmallShopAppDbContext());
var productPackedRepository = new SqlRepository<ProductPacked>(new SmallShopAppDbContext());

productWeightedRepository.Add(new ProductWeighted { Name = "jablka", Price = 1.3f, Weight = 2.5f });
productWeightedRepository.Add(new ProductWeighted { Name = "ziemniaki", Price = 5, Weight = 1.5f });
productWeightedRepository.Save();

productPackedRepository.Add(new ProductPacked { Name = "sok", Price = 6, Quantity = 1 });
productPackedRepository.Add(new ProductPacked { Name = "mleko", Price = 3.5f, Quantity = 2 });
productPackedRepository.Save();



WriteAllToConsole(productWeightedRepository);
WriteAllToConsole(productPackedRepository);

Console.WriteLine("Press any key to exit ...");
Console.ReadKey();

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{ 
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item.ToString());
    }
}