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
}
