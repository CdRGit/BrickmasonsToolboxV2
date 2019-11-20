using CustomProgrammingLanguage.Compiling;
using CustomProgrammingLanguage.Compiling.Nodes;
using CustomProgrammingLanguage.Compiling.Values;
using CustomProgrammingLanguage.Extensions;
using CustomProgrammingLanguage.OutputMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static CustomProgrammingLanguage.Compiling.Interpreter;

namespace BrickmasonsToolboxV2.Integrations
{
    public class CommandInterpreterExtension : InterpreterExtension
    {
        IOutput fileOutput;

        public CommandInterpreterExtension(IOutput fileOutput)
        {
            this.fileOutput = fileOutput;
        }

        public override Result VisitExtension(Node n, Context context)
        {
            if (n is SayNode)
            {
                return VisitSayNode(n as SayNode, context);
            }
            if (n is TellRawNode)
            {
                return VisitTellRawNode(n as TellRawNode, context);
            }
            if (n is MessageNode)
            {
                return VisitMessageNode(n as MessageNode, context);
            }
            if (n is TeamMessageNode)
            {
                return VisitTeamMessageNode(n as TeamMessageNode, context);
            }
            if (n is TagNode)
            {
                return VisitTagNode(n as TagNode, context);
            }
            if (n is MeNode)
            {
                return VisitMeNode(n as MeNode, context);
            }

            return base.VisitExtension(n, context);
        }

        private Result VisitMeNode(MeNode n, Context context)
        {
            Result res = new Result();
            Value action = res.Register(Visit(n.action, context));
            if (res.ShouldReturn()) return res;

            fileOutput.WriteLine("me " + action);

            return res.Success(Value.NULL);
        }

        private Result VisitTagNode(TagNode n, Context context)
        {
            Result res = new Result();
            Value entity = res.Register(Visit(n.entity, context));
            if (res.ShouldReturn()) return res;
            if (n.mode == "list")
            {
                fileOutput.WriteLine("tag " + entity + " list");

                return res.Success(Value.NULL);
            }
            else
            {
                Value tag = res.Register(Visit(n.tag, context));
                if (res.ShouldReturn()) return res;

                fileOutput.WriteLine("tag " + entity + " " + n.mode + " " + tag);

                return res.Success(Value.NULL);
            }
        }

        private Result VisitTellRawNode(TellRawNode n, Context context)
        {
            Result res = new Result();
            Value entity = res.Register(Visit(n.entity, context));
            if (res.ShouldReturn()) return res;
            Value json = res.Register(Visit(n.json, context));
            if (res.ShouldReturn()) return res;

            fileOutput.WriteLine("tellraw " + entity.ToString() + " \"" + json.ToString().Replace("\\", "\\\\").Replace("\"", "\\\"") + "\"");

            return res.Success(Value.NULL);
        }

        private Result VisitSayNode(SayNode n, Context context)
        {
            Result res = new Result();
            Value value = res.Register(Visit(n.toSay, context));
            if (res.ShouldReturn()) return res;

            fileOutput.WriteLine("say " + value.ToString());

            return res.Success(Value.NULL);
        }

        private Result VisitMessageNode(MessageNode n, Context context)
        {
            Result res = new Result();
            Value entity = res.Register(Visit(n.entity, context));
            if (res.ShouldReturn()) return res;
            Value message = res.Register(Visit(n.message, context));
            if (res.ShouldReturn()) return res;

            fileOutput.WriteLine(n.value.ToLower() + " " + entity.ToString() + " " + message.ToString());

            return res.Success(Value.NULL);
        }

        private Result VisitTeamMessageNode(TeamMessageNode n, Context context)
        {
            Result res = new Result();
            Value value = res.Register(Visit(n.toMessage, context));
            if (res.ShouldReturn()) return res;

            fileOutput.WriteLine(n.type + " " + value.ToString());

            return res.Success(Value.NULL);
        }
    }
}
