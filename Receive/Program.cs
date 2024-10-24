namespace Receive
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sub = new Receive();

            sub.ReadMessages();
        }
    }
}
