#region

using System;
using Akka.Actor;

#endregion

namespace common
{
    public class ChatActor : ReceiveActor
    {
        public ChatActor()
        {
            ReceiveAny(Console.WriteLine);
        }
    }
}