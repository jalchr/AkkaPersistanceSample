using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaPersistanceSample
{
    public class ParentActor : ReceiveActor
    {
        public ParentActor()
        {
            var actor = Context.ActorOf(Props.Create<PersistentChildActor>(), "PersistentChildActor");

            actor.Tell("Message 1");
            actor.Tell("Message 2");
            actor.Tell(new PersistentChildActor.GetMessages());

            Receive<IReadOnlyList<string>>(messages =>
            {
                Console.WriteLine("Received messages...");

                foreach (var message in messages)
                {
                    Console.WriteLine(message);
                }

                Context.System.Terminate();
            });
        }
    }

}
