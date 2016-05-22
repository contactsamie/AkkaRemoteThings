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
                //   actorSystem.ActorOf<ChatActor>(chatActorName);
                //  var router = actorSystem.ActorOf(Props.Create<ChatActor>( ).WithRouter(FromConfig.Instance), "some-group-router");
                var router = actorSystem.ActorOf(Props.Create(() => new ChatActor()), chatActorName);
                //  actorSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), chatActorName);

//                actorSystem.ActorOf(Props.Empty.WithRouter(
//    new ClusterRouterGroup(
//        new RoundRobinGroup("/user/backend"),
//        new ClusterRouterGroupSettings(10, false, "backend", ImmutableHashSet.Create("/user/backend"))
//    )
//));


                Console.WriteLine("Started SomeServerNode");
                while (true)
                {
                    Console.ReadLine();
                }
            }
        }
    }
}