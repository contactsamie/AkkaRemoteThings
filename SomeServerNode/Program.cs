using System;
using System.Configuration;
using Akka.Actor;
using common;

namespace SomeServerNode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var myActorsystem = ConfigurationManager.AppSettings["my-actorsystem"];
            var route = ConfigurationManager.AppSettings["route"];
            var chatActorName = ConfigurationManager.AppSettings["chat-actor-name"];
            using (var actorSystem = ActorSystem.Create(myActorsystem))
            {
                actorSystem.ActorOf<ChatActor>(chatActorName);
                Console.WriteLine("Started SomeServerNode");
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }
    }

   
}