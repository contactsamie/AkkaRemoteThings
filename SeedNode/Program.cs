using System;
using System.Configuration;
using Akka.Actor;
using common;

namespace SeedNode
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
                  actorSystem.ActorOf<ChatActor>("ChatActor");
                // var chatActorSelection = actorSystem.ActorSelection("akka.tcp://ChatSystemCluster@localhost:50000/user/ChatActor");
                // var chatActorRemote = chatActorSelection.ResolveOne(TimeSpan.FromSeconds(30)).Result;
                Console.WriteLine("Started SeedNode");
                while (true)
                {
                    Console.ReadLine();
                    //  chatActorRemote.Tell(Console.ReadLine());
                }
            }
        }
    }
}