using BrickmasonsToolboxV2.Integrations;
using CustomProgrammingLanguage.Compiling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickmasonsToolboxV2.Setup
{
    public static class Language
    {
        public static LanguageSpecs Setup(MainForm mainForm)
        {
            return new LanguageSpecs().IgnoreSpaces().IgnoreTabs().IncludeTrueFalse()
                .WithKeywords(("say", "SAY"), ("tellraw", "TELLRAW"), ("msg", "MSG"), ("tell", "TELL"), ("w", "W"), ("teammsg", "TEAMMSG"), ("tm", "TM"), ("tag", "TAG"),
                ("add", "ADD"), ("list", "LIST"), ("remove", "REMOVE"), ("me", "ME"), ("kill", "KILL"), ("gamemode", "GAMEMODE"), ("creative", "CREATIVE"),
                ("spectator", "SPECTATOR"), ("survival", "SURVIVAL"), ("adventure", "ADVENTURE"), ("function", "FUNCTION"), ("clear", "CLEAR"), ("difficulty", "DIFFICULTY"),
                ("easy", "EASY"), ("normal", "NORMAL"), ("hard", "HARD"), ("peaceful", "PEACEFUL"), ("effect", "EFFECT"), ("give", "GIVE"), ("xp", "XP"), ("experience", "EXPERIENCE"),
                ("add", "ADD"), ("set", "SET"), ("query", "QUERY"), ("levels", "LEVELS"), ("points", "POINTS"), ("seed", "SEED"), ("enchant", "ENCHANT"))
                .WithParserExtensions(new CommandParserExtension())
                .WithBuiltInFuctions()
                .WithInterpreterExtensions(new CommandInterpreterExtension(new MainFormOutput(mainForm)));
        }
    }
}
