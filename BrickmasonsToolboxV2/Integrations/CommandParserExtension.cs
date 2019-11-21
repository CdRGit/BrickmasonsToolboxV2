using CustomProgrammingLanguage.Compiling;
using CustomProgrammingLanguage.Compiling.Errors;
using CustomProgrammingLanguage.Compiling.Nodes;
using CustomProgrammingLanguage.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CustomProgrammingLanguage.Compiling.Parser;

namespace BrickmasonsToolboxV2.Integrations
{
    public class CommandParserExtension : ParserExtension
    {
        public override Result StatementExtension()
        {
            return CommandExtension();
        }

        protected Result CommandExtension()
        {
            Result res = new Result();
            Position start = currentToken.start;

            // Say command
            if (currentToken.Matches("TT_KEYWORD", "SAY"))
            {
                res.RegisterAdvance();
                Advance();

                Node expr = (Node)res.Register(Expr());
                if (res.error != null) return res;

                return res.Success(new SayNode(expr, start, expr.end));
            }
            // Tellraw command
            if (currentToken.Matches("TT_KEYWORD", "TELLRAW"))
            {
                res.RegisterAdvance();
                Advance();

                Node entity = (Node)res.Register(Expr());
                if (res.error != null) return res;

                Node json = (Node)res.Register(Expr());
                if (res.error != null) return res;

                return res.Success(new TellRawNode(entity, json, start, json.end));
            }
            // Message Command
            if (currentToken.Matches("TT_KEYWORD", "TELL") || currentToken.Matches("TT_KEYWORD", "MSG") || currentToken.Matches("TT_KEYWORD", "W"))
            {
                string value = currentToken.value;

                res.RegisterAdvance();
                Advance();

                Node entity = (Node)res.Register(Expr());
                if (res.error != null) return res;

                Node message = (Node)res.Register(Expr());
                if (res.error != null) return res;

                return res.Success(new MessageNode(value, entity, message, start, message.end));
            }
            // TeamMessage Command
            if (currentToken.Matches("TT_KEYWORD", "TEAMMSG"))
            {
                res.RegisterAdvance();
                Advance();

                Node expr = (Node)res.Register(Expr());
                if (res.error != null) return res;

                return res.Success(new TeamMessageNode("teammsg", expr, start, expr.end));
            }
            if (currentToken.Matches("TT_KEYWORD", "TM"))
            {
                res.RegisterAdvance();
                Advance();

                Node expr = (Node)res.Register(Expr());
                if (res.error != null) return res;

                return res.Success(new TeamMessageNode("tm", expr, start, expr.end));
            }
            // Tag Command
            if (currentToken.Matches("TT_KEYWORD", "TAG"))
            {
                res.RegisterAdvance();
                Advance();

                Node entity = (Node)res.Register(Expr());
                if (res.error != null) return res;

                // tag entity list
                if (currentToken.Matches("TT_KEYWORD", "LIST"))
                {
                    Position end = currentToken.end;

                    res.RegisterAdvance();
                    Advance();

                    // Done!
                    return res.Success(new TagNode(entity, "list", start, end));
                }
                string mode = currentToken.value.ToLower();

                res.RegisterAdvance();
                Advance();
                // tag entity (add / remove) tag_name
                Node tag = (Node)res.Register(Expr());
                if (res.error != null) return res;

                // Done!
                return res.Success(new TagNode(entity, mode, tag, start, tag.end));
            }
            // Me Command
            if (currentToken.Matches("TT_KEYWORD", "ME"))
            {
                res.RegisterAdvance();
                Advance();

                Node action = (Node)res.Register(Expr());
                if (res.error != null) return res;

                return res.Success(new MeNode(action, start, action.end));
            }
            // Kill Command
            if (currentToken.Matches("TT_KEYWORD", "KILL"))
            {
                res.RegisterAdvance();
                Advance();

                Node entity = (Node)res.Register(Expr());
                if (res.error != null) return res;

                return res.Success(new KillNode(entity, start, entity.end));
            }
            // Gamemode Command
            if (currentToken.Matches("TT_KEYWORD", "GAMEMODE"))
            {
                res.RegisterAdvance();
                Advance();

                if (currentToken.type == "TT_KEYWORD" && new string[] { "CREATIVE", "SURVIVAL", "SPECTATOR", "ADVENTURE" }.Contains(currentToken.value))
                {
                    string mode = currentToken.value.ToLower();
                    res.RegisterAdvance();
                    Advance();

                    Node entity = (Node)res.Register(Expr());
                    if (res.error != null) return res;

                    return res.Success(new GameModeNode(mode, entity, start, entity.end));
                }
                else
                {
                    return res.Failure(new InvalidSyntaxError(currentToken.start, currentToken.end, "No gamemode here"));
                }
            }
            // Function Command
            if (currentToken.Matches("TT_KEYWORD", "FUNCTION"))
            {
                res.RegisterAdvance();
                Advance();

                Node expr = (Node)res.Register(Expr());
                if (res.error != null) return res;

                return res.Success(new FunctionNode(expr, start, expr.end));
            }
            // Clear Command
            if (currentToken.Matches("TT_KEYWORD", "CLEAR"))
            {
                res.RegisterAdvance();
                Advance();

                Node entity = (Node)res.Register(Expr());
                if (res.error != null) return res;
                Node item = (Node)res.Register(Expr());
                if (res.error != null) return res;
                Node count = (Node)res.Register(Expr());
                if (res.error != null) return res;

                return res.Success(new ClearNode(entity, item, count, start, count.end));
            }
            // Difficulty Commad
            if (currentToken.Matches("TT_KEYWORD", "DIFFICULTY"))
            {
                res.RegisterAdvance();
                Advance();

                if (currentToken.Matches("TT_KEYWORD", "EASY"))
                {
                    Position end = currentToken.end;
                    res.RegisterAdvance();
                    Advance();

                    return res.Success(new DifficultyNode("easy", start, end));
                }

                if (currentToken.Matches("TT_KEYWORD", "NORMAL"))
                {
                    Position end = currentToken.end;
                    res.RegisterAdvance();
                    Advance();

                    return res.Success(new DifficultyNode("normal", start, end));
                }

                if (currentToken.Matches("TT_KEYWORD", "HARD"))
                {
                    Position end = currentToken.end;
                    res.RegisterAdvance();
                    Advance();

                    return res.Success(new DifficultyNode("hard", start, end));
                }

                if (currentToken.Matches("TT_KEYWORD", "PEACEFUL"))
                {
                    Position end = currentToken.end;
                    res.RegisterAdvance();
                    Advance();

                    return res.Success(new DifficultyNode("peaceful", start, end));
                }
            }


            return res.Failure(new InvalidSyntaxError(currentToken.start, currentToken.end, "No command found"));
        }
    }
}
