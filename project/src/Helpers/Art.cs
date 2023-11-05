using System.Linq;
using Godot.Collections;

namespace SCALE.Helpers;

public static class Art
{
    public static Dictionary<string, Color> NamedColours = new Dictionary<string, Color>()
    {
        {
            "ALICEBLUE",
            new Color(4042850303U)
        },
        {
            "ANTIQUEWHITE",
            new Color(4209760255U)
        },
        {
            "AQUA",
            new Color(16777215U)
        },
        {
            "AQUAMARINE",
            new Color(2147472639U)
        },
        {
            "AZURE",
            new Color(4043309055U)
        },
        {
            "BEIGE",
            new Color(4126530815U)
        },
        {
            "BISQUE",
            new Color(4293182719U)
        },
        {
            "BLANCHEDALMOND",
            new Color(4293643775U)
        },
        {
            "BLUE",
            new Color((uint)ushort.MaxValue)
        },
        {
            "BLUEVIOLET",
            new Color(2318131967U)
        },
        {
            "BROWN",
            new Color(2771004159U)
        },
        {
            "BURLYWOOD",
            new Color(3736635391U)
        },
        {
            "CADETBLUE",
            new Color(1604231423U)
        },
        {
            "CHARTREUSE",
            new Color(2147418367U)
        },
        {
            "CHOCOLATE",
            new Color(3530104575U)
        },
        {
            "CORAL",
            new Color(4286533887U)
        },
        {
            "CORNFLOWERBLUE",
            new Color(1687547391U)
        },
        {
            "CORNSILK",
            new Color(4294499583U)
        },
        {
            "CRIMSON",
            new Color(3692313855U)
        },
        {
            "CYAN",
            new Color(16777215U)
        },
        {
            "DARKBLUE",
            new Color(35839U)
        },
        {
            "DARKCYAN",
            new Color(9145343U)
        },
        {
            "DARKGOLDENROD",
            new Color(3095792639U)
        },
        {
            "DARKGREEN",
            new Color(6553855U)
        },
        {
            "DARKKHAKI",
            new Color(3182914559U)
        },
        {
            "DARKMAGENTA",
            new Color(2332068863U)
        },
        {
            "DARKOLIVEGREEN",
            new Color(1433087999U)
        },
        {
            "DARKORANGE",
            new Color(4287365375U)
        },
        {
            "DARKORCHID",
            new Color(2570243327U)
        },
        {
            "DARKRED",
            new Color(2332033279U)
        },
        {
            "DARKSALMON",
            new Color(3918953215U)
        },
        {
            "DARKSEAGREEN",
            new Color(2411499519U)
        },
        {
            "DARKSLATEBLUE",
            new Color(1211993087U)
        },
        {
            "DARKTURQUOISE",
            new Color(13554175U)
        },
        {
            "DARKVIOLET",
            new Color(2483082239U)
        },
        {
            "DEEPPINK",
            new Color(4279538687U)
        },
        {
            "DEEPSKYBLUE",
            new Color(12582911U)
        },
        {
            "DODGERBLUE",
            new Color(512819199U)
        },
        {
            "FIREBRICK",
            new Color(2988581631U)
        },
        {
            "FLORALWHITE",
            new Color(4294635775U)
        },
        {
            "FORESTGREEN",
            new Color(579543807U)
        },
        {
            "FUCHSIA",
            new Color(4278255615U)
        },
        {
            "GAINSBORO",
            new Color(3705462015U)
        },
        {
            "GOLD",
            new Color(4292280575U)
        },
        {
            "GOLDENROD",
            new Color(3668254975U)
        },
        {
            "GREEN",
            new Color(16711935U)
        },
        {
            "GREENYELLOW",
            new Color(2919182335U)
        },
        {
            "HONEYDEW",
            new Color(4043305215U)
        },
        {
            "HOTPINK",
            new Color(4285117695U)
        },
        {
            "INDIANRED",
            new Color(3445382399U)
        },
        {
            "INDIGO",
            new Color(1258324735U)
        },
        {
            "IVORY",
            new Color(4294963455U)
        },
        {
            "KHAKI",
            new Color(4041641215U)
        },
        {
            "LAVENDER",
            new Color(3873897215U)
        },
        {
            "LAVENDERBLUSH",
            new Color(4293981695U)
        },
        {
            "LAWNGREEN",
            new Color(2096890111U)
        },
        {
            "LEMONCHIFFON",
            new Color(4294626815U)
        },
        {
            "LIGHTBLUE",
            new Color(2916673279U)
        },
        {
            "LIGHTCORAL",
            new Color(4034953471U)
        },
        {
            "LIGHTCYAN",
            new Color(3774873599U)
        },
        {
            "LIGHTGOLDENROD",
            new Color(4210742015U)
        },
        {
            "LIGHTGREEN",
            new Color(2431553791U)
        },
        {
            "LIGHTPINK",
            new Color(4290167295U)
        },
        {
            "LIGHTSALMON",
            new Color(4288707327U)
        },
        {
            "LIGHTSEAGREEN",
            new Color(548580095U)
        },
        {
            "LIGHTSKYBLUE",
            new Color(2278488831U)
        },
        {
            "LIGHTSTEELBLUE",
            new Color(2965692159U)
        },
        {
            "LIGHTYELLOW",
            new Color(4294959359U)
        },
        {
            "LIME",
            new Color(16711935U)
        },
        {
            "LIMEGREEN",
            new Color(852308735U)
        },
        {
            "LINEN",
            new Color(4210091775U)
        },
        {
            "MAGENTA",
            new Color(4278255615U)
        },
        {
            "MAROON",
            new Color(2955960575U)
        },
        {
            "MEDIUMAQUAMARINE",
            new Color(1724754687U)
        },
        {
            "MEDIUMBLUE",
            new Color(52735U)
        },
        {
            "MEDIUMORCHID",
            new Color(3126187007U)
        },
        {
            "MEDIUMPURPLE",
            new Color(2473647103U)
        },
        {
            "MEDIUMSEAGREEN",
            new Color(1018393087U)
        },
        {
            "MEDIUMSLATEBLUE",
            new Color(2070474495U)
        },
        {
            "MEDIUMSPRINGGREEN",
            new Color(16423679U)
        },
        {
            "MEDIUMTURQUOISE",
            new Color(1221709055U)
        },
        {
            "MEDIUMVIOLETRED",
            new Color(3340076543U)
        },
        {
            "MIDNIGHTBLUE",
            new Color(421097727U)
        },
        {
            "MINTCREAM",
            new Color(4127193855U)
        },
        {
            "MISTYROSE",
            new Color(4293190143U)
        },
        {
            "MOCCASIN",
            new Color(4293178879U)
        },
        {
            "NAVAJOWHITE",
            new Color(4292783615U)
        },
        {
            "NAVYBLUE",
            new Color(33023U)
        },
        {
            "OLDLACE",
            new Color(4260751103U)
        },
        {
            "OLIVE",
            new Color(2155872511U)
        },
        {
            "OLIVEDRAB",
            new Color(1804477439U)
        },
        {
            "ORANGE",
            new Color(4289003775U)
        },
        {
            "ORANGERED",
            new Color(4282712319U)
        },
        {
            "ORCHID",
            new Color(3664828159U)
        },
        {
            "PALEGOLDENROD",
            new Color(4008225535U)
        },
        {
            "PALEGREEN",
            new Color(2566625535U)
        },
        {
            "PALETURQUOISE",
            new Color(2951671551U)
        },
        {
            "PALEVIOLETRED",
            new Color(3681588223U)
        },
        {
            "PAPAYAWHIP",
            new Color(4293907967U)
        },
        {
            "PEACHPUFF",
            new Color(4292524543U)
        },
        {
            "PERU",
            new Color(3448061951U)
        },
        {
            "PINK",
            new Color(4290825215U)
        },
        {
            "PLUM",
            new Color(3718307327U)
        },
        {
            "POWDERBLUE",
            new Color(2967529215U)
        },
        {
            "PURPLE",
            new Color(2686513407U)
        },
        {
            "REBECCAPURPLE",
            new Color(1714657791U)
        },
        {
            "RED",
            new Color(4278190335U)
        },
        {
            "ROSYBROWN",
            new Color(3163525119U)
        },
        {
            "ROYALBLUE",
            new Color(1097458175U)
        },
        {
            "SADDLEBROWN",
            new Color(2336560127U)
        },
        {
            "SALMON",
            new Color(4202722047U)
        },
        {
            "SANDYBROWN",
            new Color(4104413439U)
        },
        {
            "SEAGREEN",
            new Color(780883967U)
        },
        {
            "SEASHELL",
            new Color(4294307583U)
        },
        {
            "SIENNA",
            new Color(2689740287U)
        },
        {
            "SKYBLUE",
            new Color(2278484991U)
        },
        {
            "SLATEGRAY",
            new Color(1887473919U)
        },
        {
            "SNOW",
            new Color(4294638335U)
        },
        {
            "SPRINGGREEN",
            new Color(16744447U)
        },
        {
            "STEELBLUE",
            new Color(1182971135U)
        },
        {
            "TAN",
            new Color(3535047935U)
        },
        {
            "TEAL",
            new Color(8421631U)
        },
        {
            "THISTLE",
            new Color(3636451583U)
        },
        {
            "TOMATO",
            new Color(4284696575U)
        },
        {
            "TRANSPARENT",
            new Color(4294967040U)
        },
        {
            "TURQUOISE",
            new Color(1088475391U)
        },
        {
            "VIOLET",
            new Color(4001558271U)
        },
        {
            "WEBGRAY",
            new Color(2155905279U)
        },
        {
            "WEBGREEN",
            new Color(8388863U)
        },
        {
            "WEBMAROON",
            new Color(2147483903U)
        },
        {
            "WEBPURPLE",
            new Color(2147516671U)
        },
        {
            "WHEAT",
            new Color(4125012991U)
        },
        {
            "WHITE",
            new Color(uint.MaxValue)
        },
        {
            "WHITESMOKE",
            new Color(4126537215U)
        },
        {
            "YELLOW",
            new Color(4294902015U)
        },
        {
            "YELLOWGREEN",
            new Color(2597139199U)
        }
    };

    public static Color GetRandomPastelColour()
    {
        var random = new Random();
        var randomIndex = random.Next(0, NamedColours.Count);
        return NamedColours
            .Select(x => x.Value)
            .ToList()[randomIndex];
    }
}