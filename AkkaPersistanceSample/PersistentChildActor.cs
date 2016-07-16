using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Persistence;

namespace AkkaPersistanceSample
{
    public class PersistentChildActor : ReceivePersistentActor
    {
        public class GetMessages { }

        private readonly List<string> _msgs = new List<string>(); // INTERNAL STATE

        public PersistentChildActor()
        {
            // recover
            Recover<string>(str => _msgs.Add(str));

            // commands
            Command<string>(str => Persist(str, s =>
            {
                _msgs.Add(str); // Add message to in-memory after persisting
            }));
            Command<GetMessages>(get =>
            {
                IReadOnlyList<string> messages = _msgs;
                Sender.Tell(messages);
            });
        }

        public override string PersistenceId
        {
            get
            {
                return "HardCoded";
            }
        }
    }
}
