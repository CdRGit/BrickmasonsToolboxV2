﻿using CustomProgrammingLanguage.Compiling;
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

    public class DifficultyNode : Node
    {
        internal string difficulty;

        public DifficultyNode(string difficulty, Position start, Position end)
        {
            this.difficulty = difficulty;
            this.start = start;
            this.end = end;
        }
    }

    public class EffectNode : Node
    {
        internal string mode;
        internal Node entity;
        internal Node effect;
        internal Node duration;
        internal Node amplifier;
        internal Node hideParticles;

        public EffectNode(Position start, Position end, string mode, Node entity, Node effect, Node duration, Node amplifier, Node hideParticles)
        {
            this.start = start;
            this.end = end;
            this.mode = mode;
            this.entity = entity;
            this.effect = effect;
            this.duration = duration;
            this.amplifier = amplifier;
            this.hideParticles = hideParticles;
        }
    }

    public class GiveNode : Node
    {
        internal Node entity;
        internal Node item;
        internal Node count;

        public GiveNode(Position start, Position end, Node entity, Node item, Node count)
        {
            this.start = start;
            this.end = end;
            this.entity = entity;
            this.item = item;
            this.count = count;
        }
    }

    public class XPNodeAdd : Node
    {
        internal string alias;
        internal Node entity;
        internal Node amount;
        internal string type;

        public XPNodeAdd(Position start, Position end, string alias, Node entity, Node amount, string type)
        {
            this.start = start;
            this.end = end;
            this.alias = alias;
            this.entity = entity;
            this.amount = amount;
            this.type = type;
        }
    }
    public class XPNodeSet : Node
    {
        internal string alias;
        internal Node entity;
        internal Node amount;
        internal string type;

        public XPNodeSet(Position start, Position end, string alias, Node entity, Node amount, string type)
        {
            this.start = start;
            this.end = end;
            this.alias = alias;
            this.entity = entity;
            this.amount = amount;
            this.type = type;
        }
    }

    public class XPNodeQuery : Node
    {
        internal string alias;
        internal Node entity;
        internal string type;

        public XPNodeQuery(Position start, Position end, string alias, Node entity, string type)
        {
            this.start = start;
            this.end = end;
            this.alias = alias;
            this.entity = entity;
            this.type = type;
        }
    }

    public class SeedNode : Node
    {
        public SeedNode(Position start, Position end)
        {
            this.start = start;
            this.end = end;
        }
    }

    public class EnchantNode : Node
    {
        internal Node entity;
        internal Node enchantment;
        internal Node level;

        public EnchantNode(Position start, Position end, Node entity, Node enchantment, Node level)
        {
            this.start = start;
            this.end = end;
            this.entity = entity;
            this.enchantment = enchantment;
            this.level = level;
        }
    }

    public class WeatherNode : Node
    {
        internal string weather;
        internal Node duration;

        public WeatherNode(Position start, Position end, string weather, Node duration)
        {
            this.start = start;
            this.end = end;
            this.weather = weather;
            this.duration = duration;
        }
    }
}
