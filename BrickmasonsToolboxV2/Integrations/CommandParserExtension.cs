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

                return res.Success(new SayNode(expr, start, currentToken.end));
            }

            return res.Failure(new InvalidSyntaxError(currentToken.start, currentToken.end, "No command found"));
        }
    }
}
