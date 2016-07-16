using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Persistence.SqlServer;

namespace AkkaPersistanceSample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("MyActorSystem"))
            {
                var persistence = SqlServerPersistence.Get(system);
                var actor = system.ActorOf(Props.Create<ParentActor>(), "ParentActor");
               
                system.WhenTerminated.Wait();
            }

            Console.ReadLine();
        }
    }
}
