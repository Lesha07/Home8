using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

class CounterFileOperations
{
    private const string CounterFileName = "counter.txt";

    static async Task Main()
    {
        Console.WriteLine("Starting synchronous operations...");
        var syncStopwatch = Stopwatch.StartNew();
        PerformSynchronousOperations();
        syncStopwatch.Stop();
        Console.WriteLine($"Synchronous operations completed in {syncStopwatch.ElapsedMilliseconds} ms");

        Console.WriteLine("Starting asynchronous operations...");
        var asyncStopwatch = Stopwatch.StartNew();
        await PerformAsynchronousOperations();
        asyncStopwatch.Stop();
        Console.WriteLine($"Asynchronous operations completed in {asyncStopwatch.ElapsedMilliseconds} ms");
    }

    private static void PerformSynchronousOperations()
    {
        int counter = GetCounter();
        string newFileName = $"file_{counter}.txt";

        CreateFile(newFileName, counter);
        IncrementCounter(counter);
        DeleteFile(newFileName);
    }

    private static async Task PerformAsynchronousOperations()
    {
        int counter = await GetCounterAsync();
        string newFileName = $"file_{counter}.txt";

        await CreateFileAsync(newFileName, counter);
        await IncrementCounterAsync(counter);
        await DeleteFileAsync(newFileName);
    }

    private static int GetCounter()
    {
        if (!File.Exists(CounterFileName))
        {
            File.WriteAllText(CounterFileName, "1");
            return 1;
        }

        string counterText = File.ReadAllText(CounterFileName);
        return int.Parse(counterText);
    }

    private static async Task<int> GetCounterAsync()
    {
        if (!File.Exists(CounterFileName))
        {
            await File.WriteAllTextAsync(CounterFileName, "1");
            return 1;
        }

        string counterText = await File.ReadAllTextAsync(CounterFileName);
        return int.Parse(counterText);
    }

    private static void CreateFile(string fileName, int counter)
    {
        File.WriteAllText(fileName, counter.ToString());
    }

    private static async Task CreateFileAsync(string fileName, int counter)
    {
        await File.WriteAllTextAsync(fileName, counter.ToString());
    }

    private static void IncrementCounter(int currentCounter)
    {
        int newCounter = currentCounter + 1;
        File.WriteAllText(CounterFileName, newCounter.ToString());
    }

    private static async Task IncrementCounterAsync(int currentCounter)
    {
        int newCounter = currentCounter + 1;
        await File.WriteAllTextAsync(CounterFileName, newCounter.ToString());
    }

    private static void DeleteFile(string fileName)
    {
        File.Delete(fileName);
    }

    private static async Task DeleteFileAsync(string fileName)
    {
        await Task.Run(() => File.Delete(fileName));
    }
}
