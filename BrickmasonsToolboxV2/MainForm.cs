using BrickmasonsToolboxV2.Integrations;
using BrickmasonsToolboxV2.Setup;
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
            LanguageSpecs specs = Language.Setup(this);

            Compiler.Run("<box>", textBox1Temp.Text, specs, new Interpreter.Context(new SymbolTable(), "<program>", null, new Position(), specs), new MainFormOutput(this));
        }
    }
}
