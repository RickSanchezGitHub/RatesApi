using Microsoft.Extensions.DependencyInjection;
using NLog;
using RatesApi;
using RatesApi.Core;
using RatesApi.Services;
using System.Runtime.InteropServices;

class Program
{
    public static async Task Main(string[] args)
    {
        var startUp = new StartUp();
        startUp.Start();
        Console.ReadKey();
    }
}
