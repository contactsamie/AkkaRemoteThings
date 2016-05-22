#region

using common;

#endregion

namespace client
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ChatActorSystemFactory.InitiatePeerChat();
        }
    }
}