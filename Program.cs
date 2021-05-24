using System;

namespace helloworld
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var game = new BaseGame();
                game.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occured - {exception.Message} | {exception}");
            }
        }
    }
}
