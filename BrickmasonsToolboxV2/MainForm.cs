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
            LanguageSpecs specs = new LanguageSpecs().IgnoreSpaces().IgnoreTabs().IncludeTrueFalse()
                .WithKeywords(("say", "SAY"), ("tellraw","TELLRAW"), ("msg", "MSG"), ("tell", "TELL"), ("w", "W"), ("teammsg", "TEAMMSG"), ("tm", "TM"), ("tag", "TAG"), 
                ("add", "ADD"), ("list", "LIST"), ("remove", "REMOVE"), ("me", "ME"), ("kill", "KILL"), ("gamemode", "GAMEMODE"), ("creative", "CREATIVE"), 
                ("spectator", "SPECTATOR"), ("survival", "SURVIVAL"), ("adventure", "ADVENTURE"), ("function", "FUNCTION"), ("clear", "CLEAR"), ("difficulty", "DIFFICULTY"),
                ("easy", "EASY"), ("normal", "NORMAL"), ("hard", "HARD"), ("peaceful", "PEACEFUL"), ("effect", "EFFECT"), ("give", "GIVE"), ("xp", "XP"), ("experience", "EXPERIENCE"),
                ("add", "ADD"), ("set", "SET"), ("query", "QUERY"), ("levels", "LEVELS"), ("points", "POINTS"), ("seed", "SEED"))
                .WithParserExtensions(new CommandParserExtension())
                .WithBuiltInFuctions()
                .WithInterpreterExtensions(new CommandInterpreterExtension(new MainFormOutput(this)));

            Compiler.Run("<box>", textBox1Temp.Text, specs, new Interpreter.Context(new SymbolTable(), "<program>", null, new Position(), specs), new MainFormOutput(this));
        }
    }
}
