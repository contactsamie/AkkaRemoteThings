using System;
using System.Collections.Immutable;
using System.Configuration;
using System.Linq;
using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Routing;
using Akka.Routing;
using common;

namespace SomeNode
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
             
                var chatActorSelection =actorSystem.ActorSelection("akka.tcp://ChatSystemCluster@localhost:50000/user/*");
                var chatActorSelection2 =actorSystem.ActorSelection("akka.tcp://ChatSystemCluster@localhost:50000/user/ChatActor");
                var chatActorSelection3 = actorSystem.ActorSelection("/ChatSystemCluster/user/ChatActor");
                
                //   var router = actorSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance),chatActorName);

                Console.WriteLine("Started SomeNode");
                while (true)
                {
                    var mesage = Console.ReadLine();

                    Console.WriteLine("Sending message to * selection");
                    chatActorSelection.Tell(mesage);

                    Console.WriteLine("Sending message to specific actor");
                    chatActorSelection2.Tell(mesage);

                    Console.WriteLine("Sending message to actor without protocol part");
                    chatActorSelection3.Tell(mesage);

                    Console.WriteLine("Sending message to cluster specific role");
                    var address = Cluster.Get(actorSystem).ReadView.Members.First(x => x.Roles.Contains("api")).Address;
                    var router = actorSystem.ActorSelection(address + "/user/" + chatActorName);
                    router.Tell(mesage);
                }
            }
        }
    }
}