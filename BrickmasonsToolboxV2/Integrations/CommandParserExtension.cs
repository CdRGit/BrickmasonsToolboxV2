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

                return res.Success(new TeamMessageNode(expr, start, expr.end));
            }

            return res.Failure(new InvalidSyntaxError(currentToken.start, currentToken.end, "No command found"));
        }
    }
}
