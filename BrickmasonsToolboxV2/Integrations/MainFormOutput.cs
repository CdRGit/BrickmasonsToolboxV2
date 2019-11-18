using CustomProgrammingLanguage.OutputMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickmasonsToolboxV2.Integrations
{
    public class MainFormOutput : IOutput
    {
        private MainForm form;

        public MainFormOutput(MainForm form)
        {
            this.form = form;
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public void Write(string text)
        {
            throw new NotImplementedException();
        }

        public void WriteLine(string text)
        {
            throw new NotImplementedException();
        }
    }
}
