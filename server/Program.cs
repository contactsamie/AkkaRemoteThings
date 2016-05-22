#region

using common;

#endregion

namespace server
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ChatActorSystemFactory.InitiatePeerChat();
        }
    }
}