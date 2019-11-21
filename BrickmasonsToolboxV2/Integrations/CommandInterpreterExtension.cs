using CustomProgrammingLanguage.Compiling;
using CustomProgrammingLanguage.Compiling.Nodes;
using CustomProgrammingLanguage.Compiling.Values;
using CustomProgrammingLanguage.Compiling.Errors;
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
            if (n is KillNode)
            {
                return VisitKillNode(n as KillNode, context);
            }
            if (n is GameModeNode)
            {
                return VisitGameModeNode(n as GameModeNode, context);
            }
            if (n is FunctionNode)
            {
                return VisitFunctionNode(n as FunctionNode, context);
            }
            if (n is ClearNode)
            {
                return VisitClearNode(n as ClearNode, context);
            }

            return base.VisitExtension(n, context);
        }

        private Result VisitClearNode(ClearNode n, Context context)
        {
            Result res = new Result();
            Value entity = res.Register(Visit(n.entity, context));
            if (res.ShouldReturn()) return res;
            Value item = res.Register(Visit(n.item, context));
            if (res.ShouldReturn()) return res;
            Value count = res.Register(Visit(n.count, context));
            if (res.ShouldReturn()) return res;

            if (count is Number)
            {
                fileOutput.WriteLine("clear " + entity.ToString() + " " + item.ToString() + " " + count.ToString());

                return res.Success(Value.NULL);
            }

            return res.Failure(new RuntimeError(n.count.start, n.count.end, "Count was meant to be a number", context));
        }

        private Result VisitFunctionNode(FunctionNode n, Context context)
        {
            Result res = new Result();
            Value expr = res.Register(Visit(n.expr, context));
            if (res.ShouldReturn()) return res;

            fileOutput.WriteLine("function " + expr.ToString());

            return res.Success(Value.NULL);
        }

        private Result VisitGameModeNode(GameModeNode n, Context context)
        {
            Result res = new Result();
            Value entity = res.Register(Visit(n.entity, context));
            if (res.ShouldReturn()) return res;

            fileOutput.WriteLine("gamemode " + n.mode + " " + entity);

            return res.Success(Value.NULL);
        }

        private Result VisitKillNode(KillNode n, Context context)
        {
            Result res = new Result();
            Value entity = res.Register(Visit(n.entity, context));
            if (res.ShouldReturn()) return res;

            fileOutput.WriteLine("kill " + entity);

            return res.Success(Value.NULL);
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
