using SmallShopApp.DataProviders;
using SmallShopApp.Entities;
using SmallShopApp.Repositories;
using SmallShopApp.UserCommunication;
using System.Xml.Linq;

namespace SmallShopApp
{
    public class App : IApp
    {
        private readonly IRepository<ProductWeighted> _productsWeightedRepository;
        private readonly IRepository<ProductPacked> _productsPackedRepository;
        private readonly IProductsProvider _productsProvider;
        private readonly IUserCommunication _userCommunication;

        public App(
            IRepository<ProductWeighted> productsWeightedRepository, 
            IRepository<ProductPacked> productsPackedRepository,
            IProductsProvider productsProvider,
            IUserCommunication userCommunication)
        {
            _productsWeightedRepository = productsWeightedRepository;
            _productsPackedRepository = productsPackedRepository;
            _productsProvider = productsProvider;
            _userCommunication = userCommunication;
        }

        public void Run()
        {
            _productsWeightedRepository.LoadAll();
            _productsPackedRepository.LoadAll();

            var option = _userCommunication.SelectOption();
            while (option != "q")
            {
                switch (option)
                {
                    case "1":
                        Console.WriteLine("Wyszukaj produkt:");
                        var value = _userCommunication.GetValue();
                        var itemsW = _productsProvider.WhereNameSartsWIthWP(value);//.OrderByNameW();
                        var itemsP = _productsProvider.WhereNameSartsWIthPP(value);//.OrderByNameP();
                        foreach (var item in itemsW)
                            Console.WriteLine(item.ToString());
                        foreach (var item in itemsP)
                            Console.WriteLine(item.ToString());
                        break;
                    case "2":
                        Console.WriteLine("Nazwy unikatowe:");
                        var names = _productsProvider.GetProductNames();
                        foreach (var name in names)
                            Console.WriteLine(name);
                        break;
                    case "3":
                        Console.WriteLine("Cena minimalna:");
                        var minprice = _productsProvider.GetMinimumProductPrice();
                        Console.WriteLine($"minprice: {minprice}");
                        break;
                    case "q":
                        Console.WriteLine("Do zobaczenia");
                        break;
                    default:
                        Console.WriteLine("Nie ma takiej opcji");
                        break;
                }
                option = _userCommunication.SelectOption();
            }
            






            //var pW = new ProductWeighted { Name = "pW", Price = 3.5f, Weight = 1.5f };
            //var pP = new ProductPacked { Name = "pP", Price = 3.5f, Quantity = 3 };
            //_productsWeightedRepository.Add(pW);
            //_productsPackedRepository.Add(pP);
            //_productsWeightedRepository.Save();
            //_productsPackedRepository.Save();
            //var itemsW = _productsWeightedRepository.GetAll();
            //var itemsP = _productsPackedRepository.GetAll();

        }
    }
}
