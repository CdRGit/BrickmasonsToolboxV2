using CustomProgrammingLanguage.Compiling;
using CustomProgrammingLanguage.Compiling.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickmasonsToolboxV2.Integrations
{
    public class SayNode : Node
    {
        internal Node toSay;

        public SayNode(Node toSay, Position start, Position end)
        {
            this.toSay = toSay;
            this.start = start;
            this.end = end;
        }
    }

    public class TellRawNode : Node
    {
        internal Node entity;
        internal Node json;

        public TellRawNode(Node entity, Node json, Position start, Position end)
        {
            this.entity = entity;
            this.json = json;
            this.start = start;
            this.end = end;
        }
    }

    public class MessageNode : Node
    {
        internal string value;
        internal Node entity;
        internal Node message;

        public MessageNode(string value, Node entity, Node message, Position start, Position end)
        {
            this.value = value;
            this.entity = entity;
            this.message = message;
            this.start = start;
            this.end = end;
        }
    }

    public class TeamMessageNode : Node
    {
        internal Node toMessage;

        public TeamMessageNode(Node toMessage, Position start, Position end)
        {
            this.toMessage = toMessage;
            this.start = start;
            this.end = end;
        }
    }
}
