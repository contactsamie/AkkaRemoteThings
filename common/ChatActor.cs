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
            ReceiveAny(_ =>
            {
                Console.WriteLine("From : "+Self);
                Console.WriteLine(_);
            });
        }
    }
}