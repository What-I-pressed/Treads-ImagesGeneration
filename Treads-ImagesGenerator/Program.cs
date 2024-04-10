using System.Net;
using System.Runtime.CompilerServices;

internal class Program
{
    static string _dir = Directory.GetCurrentDirectory();

    private static void Main(string[] args)
    {
        Console.WriteLine(_dir);
        Console.WriteLine("Threading is about to start");
        Start();
        Console.WriteLine("Pictures are downloaded");
    }

    static void Start()
    {
        Thread[] threads = new Thread[1000];
        for(int i = 0; i < 1000; i++)
        {
            threads[i] = new Thread(GenerateAndSave);
            threads[i].Start();
        }
        for(int i = 0; i < threads.Length; i++)
        {
            if (threads[i].ThreadState == ThreadState.Stopped) continue;
            i = -1;
            Thread.Sleep(2);
        }
    }

    static void GenerateAndSave()
    {
        using(WebClient client = new WebClient())
        {
            client.DownloadFile(new Uri("http://loremflickr.com/320/240"), Path.Combine(_dir, "pictures", Guid.NewGuid().ToString() + ".jpg"));
        }
    }
}