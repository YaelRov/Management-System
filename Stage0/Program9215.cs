partial class Program
{
    private static void Main(string[] args)
    {
        Welcome9215();
        Welcome2620();
        Console.ReadKey();
    }
    static partial void Welcome2620();
    private static void Welcome9215()
    {
        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine($"{name}, welcome to my first console application");
    }


}