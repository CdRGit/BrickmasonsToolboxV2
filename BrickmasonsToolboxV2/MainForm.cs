using BrickmasonsToolboxV2.Integrations;
using CustomProgrammingLanguage.Compiling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BrickmasonsToolboxV2
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        public void Write(string str)
        {
            MessageBox.Show(str);
        }

        public void WriteLine(string str)
        {
            MessageBox.Show(str);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            LanguageSpecs specs = new LanguageSpecs().IgnoreSpaces().IgnoreTabs().IncludeTrueFalse().WithKeywords(("say", "SAY")).WithParserExtensions(new CommandParserExtension()).WithBuiltInFuctions();

            Compiler.Run("<box>", textBox1Temp.Text, specs, new Interpreter.Context(new SymbolTable(), "<program>", null, new Position()), new MainFormOutput(this));
        }
    }
}
