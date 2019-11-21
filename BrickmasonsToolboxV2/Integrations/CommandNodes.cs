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
        internal string type;
        internal Node toMessage;

        public TeamMessageNode(string type, Node toMessage, Position start, Position end)
        {
            this.type = type;
            this.toMessage = toMessage;
            this.start = start;
            this.end = end;
        }
    }

    public class TagNode : Node
    {
        internal Node entity;
        internal string mode;
        internal Node tag;

        public TagNode(Node entity, string mode, Node tag, Position start, Position end)
        {
            this.entity = entity;
            this.mode = mode;
            this.tag = tag;
            this.start = start;
            this.end = end;
        }

        public TagNode(Node entity, string mode, Position start, Position end) : this(entity, mode, null, start, end)
        {

        }
    }

    public class MeNode : Node
    {
        internal Node action;

        public MeNode(Node action, Position start, Position end)
        {
            this.action = action;
            this.start = start;
            this.end = end;
        }
    }

    public class KillNode : Node
    {
        internal Node entity;

        public KillNode(Node entity, Position start, Position end)
        {
            this.entity = entity;
            this.start = start;
            this.end = end;
        }
    }

    public class GameModeNode : Node
    {
        internal string mode;
        internal Node entity;

        public GameModeNode(string mode, Node entity, Position start, Position end)
        {
            this.mode = mode;
            this.entity = entity;
            this.start = start;
            this.end = end;
        }
    }

    public class FunctionNode : Node
    {
        internal Node expr;

        public FunctionNode(Node expr, Position start, Position end)
        {
            this.expr = expr;
            this.start = start;
            this.end = end;
        }
    }

    public class ClearNode : Node
    {
        internal Node entity;
        internal Node item;
        internal Node count;

        public ClearNode(Node entity, Node item, Node count, Position start, Position end)
        {
            this.entity = entity;
            this.item = item;
            this.count = count;
            this.start = start;
            this.end = end;
        }
    }
}
