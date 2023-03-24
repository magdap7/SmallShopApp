using SmallShopApp;
using SmallShopApp.Data;
using SmallShopApp.Entities;
using SmallShopApp.Entities.Extensions;
using SmallShopApp.Repositories;
using System.Text.Json;

class Program
{
    static void Main(string[] args)
    {
        //var productWeightedSqlRepository = new SqlRepository<ProductWeighted>(new SmallShopAppDbContext());
        //var productPackedSqlRepository = new SqlRepository<ProductPacked>(new SmallShopAppDbContext());
        var productWeightedListRepository = new ListRepository<ProductWeighted>();
        var productPackedListRepository = new ListRepository<ProductPacked>();

        WriteLineColor(ConsoleColor.Green, "Witam w programie Mój Mały Sklep!\n");
        Console.WriteLine("Program umożliwia dodawanie, wyświetlanie lub usuwanie produktów z bazy danych / pliku.\n");
        Console.WriteLine("Wybierz jedną z opcji:");
        Console.WriteLine("(1) Dodaj produkt");
        Console.WriteLine("(2) Usuń produkt");
        Console.WriteLine("(3) Wyświetl listę produktów");
        Console.WriteLine("(q) Wyjdź z programu");
        Console.WriteLine();

        var option = Console.ReadLine();
        while (option!=null || option != "q")
        {
            switch (option)
            {
                case "1":
                    Console.WriteLine("Dodawanie produktu. Wybierz typ produku\n");
                    Console.WriteLine("1) Produkt na wagę");
                    Console.WriteLine("2) Produkt na sztuki");
                    Console.WriteLine();

                    var option1 = Console.ReadLine();
                    if (option1 == "1")
                    {
                        Console.WriteLine("Dodawanie produktu na wagę.\n");
                        List<string> objectParams = ReadObjectFromConsole<ProductWeighted>(new ProductWeighted());
                        var p1 = objectParams[1];
                        var p2 = float.Parse(objectParams[2]);
                        var p3 = float.Parse(objectParams[0]);
                        productWeightedListRepository.Add(new ProductWeighted { Name = p1, Price = p2, Weight = p3 });
                    }
                    else if (option1 == "2")
                    {
                        Console.WriteLine("Dodawanie produktu na sztuki.\n");
                        List<string> objectParams = ReadObjectFromConsole<ProductPacked>(new ProductPacked());
                        var p1 = objectParams[1];
                        var p2 = float.Parse(objectParams[2]);
                        var p3 = int.Parse(objectParams[0]);
                        productPackedListRepository.Add(new ProductPacked { Name = p1, Price = p2, Quantity = p3});
                    }
                    else
                    { 
                        Console.WriteLine("Nie wybrano właściwej opcji.\n");
                    };

                    //productWeightedSqlRepository.Add(new ProductWeighted { Name = "jablka", Price = 1.3f, Weight = 2.5f });
                    //productWeightedSqlRepository.Save();
                    //productPackedSqlRepository.Add(new ProductPacked { Name = "sok", Price = 6, Quantity = 1 });
                    //productPackedSqlRepository.Save();

                    break;
                case "2":
                    Console.WriteLine("Usuwanie produktu\n");
                    break;
                case "3":
                    Console.WriteLine("Wyświetlenie listy produktów\n");
                    //WriteAllToConsole(productWeightedSqlRepository);
                    //WriteAllToConsole(productPackedSqlRepository);
                    WriteAllToConsole(productWeightedListRepository);
                    WriteAllToConsole(productPackedListRepository);
                    break;
                case "q":
                    Console.WriteLine("Do zobaczenia");
                    return;
                    break;
                default:
                    Console.WriteLine("Nieprawidłowy wybór.");
                    break;
            }
            Console.WriteLine("Wybierz kolejną opcję");
            option = Console.ReadLine();
        }

        List<string> ReadObjectFromConsole<T>(T obj)
        {
            //string[] keyWords = new[] { "System.String", "Int32", "Double", "Single" };
            List<string> result = new List<string>();
            var nameOfType = obj.GetType().Name;
            var properties = obj.GetType().GetProperties().ToArray();
            Console.WriteLine("Type properties for object from class: " + nameOfType);
            for (int i = 0; i < properties.Length-1; i++)
            {
                var propertyName = properties[i].ToString().Split(" ");
                Console.Write(propertyName[1] + ": ");
                var line = Console.ReadLine();
                bool condition0 = (line != "");
                bool condition1 = propertyName[0].Contains("Int32") && int.TryParse(line, out int intValue);
                bool condition2 = propertyName[0].Contains("Double") && double.TryParse(line, out double doubleValue);
                bool condition3 = propertyName[0].Contains("Single") && float.TryParse(line, out float floatValue);
                bool condition4 = propertyName[0].Contains("String") && !int.TryParse(line, out int numValue);
                if (condition0 && (condition1 || condition2 || condition3 || condition4))
                    result.Add(line);
                else
                    result.Add("0");
            }
            return result;
        }
    }


    static void WriteObjectToFile<T>(T obj)
    {
        var jsonObj = JsonSerializer.Serialize<T>(obj);
        using (var writer = File.AppendText(obj.GetType().Name + ".txt"))
        {
            writer.WriteLine(jsonObj);
        }
    }

    static List<T> ReadObjectsFromFile<T>(string file)
    {
        List<T> objList = new List<T>();
        using (var reader = File.OpenText(file))
        {
            var line = reader.ReadLine();
            if (line != null)
            {
                objList.Add(JsonSerializer.Deserialize<T>(line));
                line = reader.ReadLine();
            }
        }
        return objList;
    }

    static void WriteAllToConsole(IReadRepository<IEntity> repository)
    {
        var items = repository.GetAll();
        foreach (var item in items)
            Console.WriteLine(item.ToString());
    }

    static void WriteLineColor(ConsoleColor color, string text)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    static void tests()
    {
        List<string> objectParams = new List<string>();
        objectParams.Add("price");
        objectParams.Add("name");
        objectParams.Add("id");
        Console.WriteLine(objectParams.First());
        objectParams.Reverse();
        Console.WriteLine(objectParams.First());
    }


}

