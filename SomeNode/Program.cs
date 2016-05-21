using System;
using System.Collections.Immutable;
using System.Configuration;
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
              var actor=  actorSystem.ActorOf<ChatActor>(chatActorName);
                var chatActorSelection =actorSystem.ActorSelection("akka.tcp://ChatSystem-cluster@localhost:50000/user/*");
                var chatActorSelection2 =actorSystem.ActorSelection("akka.tcp://ChatSystem-cluster@localhost:50000/user/ChatActor");

                // var chatActorSelection22 = actorSystem.ActorSelection("akka.tcp://ChatSystem-cluster@localhost:30000/api/myClusterGroupRouter");

                //var backendRouter = actorSystem.ActorOf(Props.Empty.WithRouter(FromConfig.Instance));
                
                var chatActorRemote = chatActorSelection2.ResolveOne(TimeSpan.FromSeconds(30)).Result;

                Console.WriteLine("Started SomeNode");
                while (true)
                {
                    var mesage = Console.ReadLine();
                   // backendRouter.Tell(mesage);
                    chatActorSelection.Tell(mesage);
                    chatActorRemote.Tell(mesage);
                }
            }
        }
    }
}