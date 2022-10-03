
using Lucca.Converter;
using Lucca.Converter.Models;
using System.Globalization;

class Program
{
    static void Main(string[] args)
    {

        Console.WriteLine("Hello, World! this is back test \n");

        Console.WriteLine("You need to insert Currency you want to convert FROM, Currency you want to convert TO and the amount in this format:\n FROM;Amount;TO \n example : EUR;550;JPY");
        Console.WriteLine("\n");
        var userChoice = Console.ReadLine();
        Console.WriteLine("\n");
        Console.WriteLine("nombre de ligne de currency");
        int nbr = Convert.ToInt32(Console.ReadLine());
        List<String> list = new List<String>();
        for (int i = 0; i < nbr; i++)
        {
            list.Add(Console.ReadLine());
        }

        string exchangeRate = CurrencyConverter.GetTheShortestrateExchangeRate(userChoice, list);
        Console.WriteLine(exchangeRate);

        Console.ReadLine();
    }
}
