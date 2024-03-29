﻿using CustomProgrammingLanguage.Compiling;
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
            if (n is DifficultyNode)
            {
                return VisitDifficultyNode(n as DifficultyNode, context);
            }
            if (n is EffectNode)
            {
                return VisitEffectNode(n as EffectNode, context);
            }
            if (n is GiveNode)
            {
                return VisitGiveNode(n as GiveNode, context);
            }
            if (n is XPNodeAdd)
            {
                return VisitXPNodeAdd(n as XPNodeAdd, context);
            }
            if (n is XPNodeSet)
            {
                return VisitXPNodeSet(n as XPNodeSet, context);
            }
            if (n is XPNodeQuery)
            {
                return VisitXPNodeQuery(n as XPNodeQuery, context);
            }
            if (n is SeedNode)
            {
                return VisitSeedNode(n as SeedNode, context);
            }
            if (n is EnchantNode)
            {
                return VisitEnchantNode(n as EnchantNode, context);
            }
            if (n is WeatherNode)
            {
                return VisitWeatherNode(n as WeatherNode, context);
            }

            return base.VisitExtension(n, context);
        }

        private Result VisitWeatherNode(WeatherNode n, Context context)
        {
            Result res = new Result();
            if (n.duration == null)
            {
                fileOutput.WriteLine("weather " + n.weather);

                return res.Success(Value.NULL);
            }
            Value duration = res.Register(Visit(n.duration, context));
            if (res.ShouldReturn()) return res;

            if (duration is Number)
            {
                fileOutput.WriteLine("weather " + n.weather + " " + duration.ToString());

                return res.Success(Value.NULL);
            }

            return res.Failure(new RuntimeError(n.duration.start, n.duration.end, "Level was meant to be a number", context));
        }

        private Result VisitEnchantNode(EnchantNode n, Context context)
        {
            Result res = new Result();
            Value entity = res.Register(Visit(n.entity, context));
            if (res.ShouldReturn()) return res;
            Value enchantment = res.Register(Visit(n.enchantment, context));
            if (res.ShouldReturn()) return res;
            if (n.level != null)
            {
                Value level = res.Register(Visit(n.level, context));
                if (res.ShouldReturn()) return res;

                if (level is Number)
                {
                    fileOutput.WriteLine("enchant " + entity.ToString() + " " + enchantment.ToString() + " " + level.ToString());

                    return res.Success(Value.NULL);
                }

                return res.Failure(new RuntimeError(n.level.start, n.level.end, "Level was meant to be a number", context));
            }
            else
            {
                fileOutput.WriteLine("enchant " + entity.ToString() + " " + enchantment.ToString());
            }

            return res.Success(Value.NULL);
        }

        private Result VisitSeedNode(SeedNode n, Context context)
        {
            Result res = new Result();

            fileOutput.WriteLine("seed");

            return res.Success(Value.NULL);
        }

        private Result VisitXPNodeQuery(XPNodeQuery n, Context context)
        {
            Result res = new Result();

            Value entity = res.Register(Visit(n.entity, context));
            if (res.ShouldReturn()) return res;

            fileOutput.WriteLine(n.alias + " query " + entity.ToString() + " " + n.type);

            return res.Success(Value.NULL);
        }

        private Result VisitXPNodeAdd(XPNodeAdd n, Context context)
        {
            Result res = new Result();

            Value entity = res.Register(Visit(n.entity, context));
            if (res.ShouldReturn()) return res;
            Value amount = res.Register(Visit(n.amount, context));
            if (res.ShouldReturn()) return res;

            if (amount is Number)
            {
                if (n.type == "neither")
                    fileOutput.WriteLine(n.alias + " add " + entity.ToString() + " " + amount.ToString());
                else
                    fileOutput.WriteLine(n.alias + " add " + entity.ToString() + " " + amount.ToString() + " " + n.type);

                return res.Success(Value.NULL);
            }
            else
            {
                return res.Failure(new RuntimeError(n.amount.start, n.amount.end, "Amount was meant to be a number", context));
            }
        }

        private Result VisitXPNodeSet(XPNodeSet n, Context context)
        {
            Result res = new Result();

            Value entity = res.Register(Visit(n.entity, context));
            if (res.ShouldReturn()) return res;
            Value amount = res.Register(Visit(n.amount, context));
            if (res.ShouldReturn()) return res;

            if (amount is Number)
            {
                if (n.type == "neither")
                    fileOutput.WriteLine(n.alias + " set " + entity.ToString() + " " + amount.ToString());
                else
                    fileOutput.WriteLine(n.alias + " set " + entity.ToString() + " " + amount.ToString() + " " + n.type);

                return res.Success(Value.NULL);
            }
            else
            {
                return res.Failure(new RuntimeError(n.amount.start, n.amount.end, "Amount was meant to be a number", context));
            }
        }

        private Result VisitGiveNode(GiveNode n, Context context)
        {
            Result res = new Result();

            Value entity = res.Register(Visit(n.entity, context));
            if (res.ShouldReturn()) return res;
            Value item = res.Register(Visit(n.item, context));
            if (res.ShouldReturn()) return res;

            if (n.count == null)
            {
                fileOutput.WriteLine("give " + entity.ToString() + " " + item.ToString());

                return res.Success(Value.NULL);
            }
            Value count = res.Register(Visit(n.count, context));
            if (res.ShouldReturn()) return res;

            if (count is Number)
            {
                fileOutput.WriteLine("give " + entity.ToString() + " " + item.ToString() + " " + count);

                return res.Success(Value.NULL);
            }
            else
            {
                return res.Failure(new RuntimeError(n.count.start, n.count.end, "Count was meant to be a number", context));
            }
        }

        private Result VisitEffectNode(EffectNode n, Context context)
        {
            Result res = new Result();

            if (n.mode == "give")
            {
                Value entity = res.Register(Visit(n.entity, context));
                if (res.ShouldReturn()) return res;

                Value effect = res.Register(Visit(n.effect, context));
                if (res.ShouldReturn()) return res;

                if (n.duration == null)
                {
                    fileOutput.WriteLine("effect give " + entity.ToString() + " " + effect.ToString());

                    return res.Success(Value.NULL);
                }

                if (n.amplifier == null)
                {
                    Value duration2 = res.Register(Visit(n.duration, context));
                    if (res.ShouldReturn()) return res;

                    if (duration2 is Number)
                    {
                        fileOutput.WriteLine("effect give " + entity.ToString() + " " + effect.ToString() + " " + duration2);

                        return res.Success(Value.NULL);
                    }
                    else
                    {
                        return res.Failure(new RuntimeError(n.duration.start, n.duration.end, "Duration was meant to be a number", context));
                    }
                }

                if (n.hideParticles == null)
                {
                    Value duration2 = res.Register(Visit(n.duration, context));
                    if (res.ShouldReturn()) return res;

                    if (duration2 is Number)
                    {
                        Value amplifier = res.Register(Visit(n.amplifier, context));
                        if (res.ShouldReturn()) return res;

                        if (amplifier is Number)
                        {
                            fileOutput.WriteLine("effect give " + entity.ToString() + " " + effect.ToString() + " " + duration2 + " " + amplifier);

                            return res.Success(Value.NULL);
                        }
                        else
                        {
                            return res.Failure(new RuntimeError(n.amplifier.start, n.amplifier.end, "Amplifier was meant to be a number", context));
                        }
                    }
                    else
                    {
                        return res.Failure(new RuntimeError(n.duration.start, n.duration.end, "Duration was meant to be a number", context));
                    }
                }

                Value duration = res.Register(Visit(n.duration, context));
                if (res.ShouldReturn()) return res;

                if (duration is Number)
                {
                    Value amplifier = res.Register(Visit(n.amplifier, context));
                    if (res.ShouldReturn()) return res;

                    if (amplifier is Number)
                    {
                        Value hideParticles = res.Register(Visit(n.hideParticles, context));
                        if (res.ShouldReturn()) return res;

                        fileOutput.WriteLine("effect give " + entity.ToString() + " " + effect.ToString() + " " + duration + " " + amplifier + " " + (hideParticles.IsTrue() ? "true" : "false"));

                        return res.Success(Value.NULL);
                    }
                    else
                    {
                        return res.Failure(new RuntimeError(n.amplifier.start, n.amplifier.end, "Amplifier was meant to be a number", context));
                    }
                }
                else
                {
                    return res.Failure(new RuntimeError(n.duration.start, n.duration.end, "Duration was meant to be a number", context));
                }
            }
            else
            {
                Value entity = res.Register(Visit(n.entity, context));
                if (res.ShouldReturn()) return res;

                if (n.effect == null)
                {
                    fileOutput.WriteLine("effect clear " + entity.ToString());

                    return res.Success(Value.NULL);
                }
                Value effect = res.Register(Visit(n.effect, context));
                if (res.ShouldReturn()) return res;

                fileOutput.WriteLine("effect clear " + entity.ToString() + " " + effect.ToString());

                return res.Success(Value.NULL);
            }
        }

        private Result VisitDifficultyNode(DifficultyNode n, Context context)
        {
            Result res = new Result();

            fileOutput.WriteLine("difficulty " + n.difficulty);

            return res.Success(Value.NULL);
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
