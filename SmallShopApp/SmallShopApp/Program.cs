using Microsoft.Extensions.DependencyInjection;
using SmallShopApp;
using SmallShopApp.DataProviders;
using SmallShopApp.Entities;
using SmallShopApp.Repositories;
using SmallShopApp.UserCommunication;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IRepository<ProductWeighted>, ListRepository<ProductWeighted>>();
services.AddSingleton<IRepository<ProductPacked>, ListRepository<ProductPacked>>();
services.AddSingleton<IProductsProvider, ProductsProvider>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>()!;
app.Run();

//using SmallShopApp;
//using SmallShopApp.Data;
//using SmallShopApp.Entities;
//using SmallShopApp.Entities.Extensions;
//using SmallShopApp.Repositories.Extensions;
//using System.Text.Json;

//class Program
//{
//    static void Main(string[] args)
//    {

//        //var logFilePath = Path.GetFullPath("SmallShopApp\\..\\..\\..\\..\\Data\\");
//        var productWeightedListRepository = new ListRepository<ProductWeighted>();
//        var productPackedListRepository = new ListRepository<ProductPacked>();
//        productWeightedListRepository.LoadAll();
//        productPackedListRepository.LoadAll();
//        productWeightedListRepository.ItemAdded += RepositoryOnItemAdded;
//        productPackedListRepository.ItemAdded += RepositoryOnItemAdded;
//        productWeightedListRepository.ItemRemoved += RepositoryOnItemRemoved;
//        productPackedListRepository.ItemRemoved += RepositoryOnItemRemoved;

//        WriteLineColor(ConsoleColor.Green, "Witam w programie Mój Mały Sklep!\n");
//        Console.WriteLine("Program umożliwia dodawanie, wyświetlanie lub usuwanie produktów z bazy danych / pliku.\n");
//        

//        var option = Console.ReadLine();
//        while (option != null || option != "q")
//        {
//            switch (option)
//            {
//                case "1":
//                    WriteLineColor(ConsoleColor.Green, "Dodawanie produktu. Wybierz typ produku\n");
//                    Console.WriteLine("1) Produkt na wagę");
//                    Console.WriteLine("2) Produkt na sztuki");
//                    Console.WriteLine();

//                    var option1 = Console.ReadLine();
//                    if (option1 == "1")
//                    {
//                        WriteLineColor(ConsoleColor.Green, "Dodawanie produktu na wagę.\n");
//                        List<string> objectParams = ReadObjectFromConsole<ProductWeighted>(new ProductWeighted());
//                        var p1 = objectParams[1];
//                        var p2 = float.Parse(objectParams[2]);
//                        var p3 = float.Parse(objectParams[0]);
//                        productWeightedListRepository.Add(new ProductWeighted { Name = p1, Price = p2, Weight = p3 });
//                    }
//                    else if (option1 == "2")
//                    {
//                        WriteLineColor(ConsoleColor.Green, "Dodawanie produktu na sztuki.\n");
//                        List<string> objectParams = ReadObjectFromConsole<ProductPacked>(new ProductPacked());
//                        var p1 = objectParams[1];
//                        var p2 = float.Parse(objectParams[2]);
//                        var p3 = int.Parse(objectParams[0]);
//                        productPackedListRepository.Add(new ProductPacked { Name = p1, Price = p2, Quantity = p3 });
//                    }
//                    else
//                    {
//                        WriteLineColor(ConsoleColor.Red, "Nie wybrano właściwej opcji.\n");
//                    };
//                    break;
//                case "2":
//                    WriteLineColor(ConsoleColor.Green, "Usuwanie produktu.  Wybierz typ produku\n");
//                    Console.WriteLine("1) Produkt na wagę");
//                    Console.WriteLine("2) Produkt na sztuki");
//                    Console.WriteLine();

//                    string line;
//                    var option2 = Console.ReadLine();
//                    if (option2 == "1")
//                    {
//                        WriteLineColor(ConsoleColor.Green, "Usuwanie produktu na wagę.\n");
//                        Console.WriteLine("Podaj ID produktu\n");
//                        line = Console.ReadLine();
//                        while (!int.TryParse(line, out int value))
//                        {
//                            WriteLineColor(ConsoleColor.Red, "Niepoprawny format ID, spróbuj ponownie\n");
//                            line = Console.ReadLine();
//                        }
//                        try
//                        {
//                            productWeightedListRepository.Remove(productWeightedListRepository.GetById(int.Parse(line)));
//                        }
//                        catch (Exception ex) { WriteLineColor(ConsoleColor.Red, ex.Message); };
//                    }
//                    else if (option2 == "2")
//                    {
//                        WriteLineColor(ConsoleColor.Green, "Usuwanie produktu na sztuki.\n");
//                        Console.WriteLine("Podaj ID produktu\n");
//                        line = Console.ReadLine();
//                        while (!int.TryParse(line, out int value))
//                        {
//                            WriteLineColor(ConsoleColor.Red, "Niepoprawny format ID, spróbuj ponownie\n");
//                            line = Console.ReadLine();
//                        }
//                        try
//                        {
//                            productPackedListRepository.Remove(productPackedListRepository.GetById(int.Parse(line)));
//                        }
//                        catch (Exception ex) { WriteLineColor(ConsoleColor.Red, ex.Message); };
//                    }
//                    else
//                    {
//                        WriteLineColor(ConsoleColor.Red, "Nie wybrano właściwej opcji.\n");
//                    }
//                    break;
//                case "3":
//                    Console.WriteLine("Wyświetlenie listy produktów\n");
//                    WriteAllToConsole(productWeightedListRepository);
//                    WriteAllToConsole(productPackedListRepository);
//                    break;
//                case "q":
//                    productWeightedListRepository.Save();
//                    productPackedListRepository.Save();
//                    Console.WriteLine("Do zobaczenia");
//                    return;
//                    break;
//                default:
//                    WriteLineColor(ConsoleColor.Red, "Nieprawidłowy wybór.");
//                    break;
//            }
//            Console.WriteLine("Wybierz kolejną opcję");
//            option = Console.ReadLine();
//        }

//        List<string> ReadObjectFromConsole<T>(T obj)
//        {
//            //string[] keyWords = new[] { "System.String", "Int32", "Double", "Single" };
//            List<string> result = new List<string>();
//            var nameOfType = obj.GetType().Name;
//            var properties = obj.GetType().GetProperties().ToArray();
//            Console.WriteLine("Type properties for object from class: " + nameOfType);
//            for (int i = 0; i < properties.Length - 1; i++)
//            {
//                var propertyName = properties[i].ToString().Split(" ");
//                Console.Write(propertyName[1] + ": ");
//                var line = Console.ReadLine();
//                bool condition0 = (line != "");
//                bool condition1 = propertyName[0].Contains("Int32") && int.TryParse(line, out int intValue);
//                bool condition2 = propertyName[0].Contains("Double") && double.TryParse(line, out double doubleValue);
//                bool condition3 = propertyName[0].Contains("Single") && float.TryParse(line, out float floatValue);
//                bool condition4 = propertyName[0].Contains("String") && !int.TryParse(line, out int numValue);
//                if (condition0 && (condition1 || condition2 || condition3 || condition4))
//                    result.Add(line);
//                else
//                    result.Add("0");
//            }
//            return result;
//        }

//        void RepositoryOnItemAdded<T>(object? sender, T obj)
//        {
//            var fileName = obj.GetType().Name + "s";
//            var currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd hh.mm.ss");
//            var lineToFile = $"Object: {obj.GetType().Name}\t Sender: {sender.GetType().Name}\t Action: Added";
//            using (var writer = File.AppendText("_Log" + fileName + ".txt"))
//            {
//                writer.WriteLine(currentDate + "\t" + lineToFile);
//            }
//            WriteLineColor(ConsoleColor.Blue, currentDate + "\t" + lineToFile);
//        }

//        void RepositoryOnItemRemoved<T>(object? sender, T obj)
//        {
//            var fileName = obj.GetType().Name + "s";
//            var currentDate = DateTime.UtcNow.ToString("yyyy-MM-dd hh.mm.ss");
//            var lineToFile = $"Object: {obj.GetType().Name}\t Sender: {sender.GetType().Name}\t Action: Removed";
//            using (var writer = File.AppendText("_Log" + fileName + ".txt"))
//            {
//                writer.WriteLine(currentDate + "\t" + lineToFile);
//            }
//            WriteLineColor(ConsoleColor.Blue, currentDate + "\t" + lineToFile);
//        }
//    }

//    static void WriteAllToConsole(IReadRepository<IEntity> repository)
//    {
//        var items = repository.GetAll();
//        foreach (var item in items)
//            Console.WriteLine(item.ToString());
//    }


//}

