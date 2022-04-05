using RatesApi;

class Program
{
    public static void Main(string[] args)
    {
        var startUp = new StartUp();
        startUp.Start().Wait();
        Console.ReadKey();
    }
}
