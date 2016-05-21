using System;
using System.Configuration;
using Akka.Actor;

namespace common
{
    public class ChatActorSystemFactory
    {
        public static void InitiatePeerChat()
        {
            var myActorsystem = ConfigurationManager.AppSettings["my-actorsystem"];
            var remote = ConfigurationManager.AppSettings["remote"];
            var chatActorName = ConfigurationManager.AppSettings["chat-actor-name"];
            using (var actorSystem = ActorSystem.Create(myActorsystem))
            {
                actorSystem.ActorOf<ChatActor>(chatActorName);
                var chatActorSelection = actorSystem.ActorSelection(remote + "/user/" + chatActorName);
                var chatActorRemote = chatActorSelection.ResolveOne(TimeSpan.FromSeconds(3)).Result;
                while (true)
                {
                    chatActorRemote.Tell(Console.ReadLine());
                }
            }
        }
    }
}