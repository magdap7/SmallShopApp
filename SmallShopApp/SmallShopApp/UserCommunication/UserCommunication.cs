using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallShopApp.UserCommunication
{
    public class UserCommunication : IUserCommunication
    {
        public UserCommunication() { }

        public string GetValue()
        {
            Console.WriteLine("Podaj parametr (fragment nazwy albo liczbę:");
            var option = Console.ReadLine();
            return option;
        }

        public string SelectOption()
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Program Mini Sklep");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("Wybierz jedną z opcji:");
            Console.WriteLine("(1) Wyszukaj produkt");
            Console.WriteLine("(2) Wyświetl unikatowe nazwy produktów");
            Console.WriteLine("(3) Posortuj produkty według ceny");
            Console.WriteLine("(q) Aby wyjść z programu");
            Console.WriteLine();

            var option = Console.ReadLine();
            return option;
        }

    }
}
